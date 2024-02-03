using System.Net;
using System.Net.Sockets;
using InjaData.Models;
using InjaDTO;

namespace InjaAPI;

public static class Helper
{
  public static string TokenSecret { get; set; } = "!qwert.@.123456!";
  public static string JWTIssuer { get; set; } = "inja-api";
  public static string JWTAudience { get; set; } = "inja-api";
  public static string JWTClaimId { get; set; } = "Id";
  public static string JWTClaimUserName { get; set; } = "UserName";
  public static string JWTClaimRoles { get; set; } = "Roles";
  public static string JWTClaimEmail { get; set; } = "EMail";

  public static string APIDomain { get; set; } = "https://api.beautycomp.com";
  public static string BrevoKey { get; set; } = string.Empty;
  public static string ChangePasswordURL { get; set; } = string.Empty;
  public static string MailNameFrom { get; set; } = string.Empty;
  public static string MailAddressFrom { get; set; } = string.Empty;

  public static string[] Languages => new[] { "En", "Es", "It", "Fr", "Pr" };

  public static CriteriaNamesDTO GetCriteriaNames(int criteriaId, Dictionary<int, Judgmentcriterion> dicCriterias)
  {
    var result = new CriteriaNamesDTO();
    if (!dicCriterias.TryGetValue(criteriaId, out var value)) return result;

    result.en = value.Name;
    result.es = value.Namees ?? string.Empty;
    result.fr = value.Namefr ?? string.Empty;
    result.it = value.Nameit ?? string.Empty;
    result.pt = value.Namepr ?? string.Empty;
    return result;
  }
  
  public static string GetRemoteIpAddress(HttpContext httpContext)
  {
    var remoteIpAddress = new [] { string.Empty, string.Empty, string.Empty };
    var remoteIpAddressUsingConnectionRemoteIpAddress = GetRemoteHostIpAddressUsingRemoteIpAddress(httpContext);
    var remoteIpAddressUsingXForwardedFor = GetRemoteHostIpAddressUsingXForwardedFor(httpContext);
    var remoteIpAddressUsingXRealIp = GetRemoteHostIpAddressUsingXRealIp(httpContext);

    if (remoteIpAddressUsingConnectionRemoteIpAddress != null)
    {
      remoteIpAddress[0] = remoteIpAddressUsingConnectionRemoteIpAddress.ToString();
    }

    if (remoteIpAddressUsingXForwardedFor != null)
    {
      remoteIpAddress[1] = remoteIpAddressUsingXForwardedFor.ToString();
    }

    if (remoteIpAddressUsingXRealIp != null)
    {
      remoteIpAddress[2] = remoteIpAddressUsingXRealIp.ToString();
    }

    return string.Join(", ", remoteIpAddress);
  }

  public static async Task<Tuple<int, CityDTO?>> AddCity(dbContext _context, string aCityName, string? aCountryName)
  {
    var dbCity = _context.Cities.ToList().FirstOrDefault(x => string.Equals(x.Name, aCityName, StringComparison.InvariantCultureIgnoreCase));

    var rdbCountry = await Helper.AddCountry(_context, aCountryName ?? "Desconocido");
    var dbCountry = rdbCountry.Item2;

    if (dbCity != null)
      return Tuple.Create(200, CityDTO.FromDbItem(dbCity))!;

    dbCity = new City
    {
      Name = aCityName,
      Countryid = dbCountry!.Id,
      Active = true
    };
		
    _context.Cities.Add(dbCity);
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, $"Error agregando un Ciudad {aCityName}");
      return Tuple.Create(StatusCodes.Status500InternalServerError, (CityDTO?)null);
    }

    return Tuple.Create(200, CityDTO.FromDbItem(dbCity))!;
  }
  
  public static async Task<Tuple<int, CountryDTO?>> AddCountry(dbContext _context, string aCountryName)
  {
    var dbCountry = _context.Countries.ToList().FirstOrDefault(x =>
      string.Equals(x.Name, aCountryName, StringComparison.InvariantCultureIgnoreCase));

    if (dbCountry != null) return Tuple.Create(200, CountryDTO.FromDbItem(dbCountry))!;
    dbCountry = new Country
    {
      Active = true,
      Name = aCountryName
    };
    _context.Countries.Add(dbCountry);
    try
    {
      await _context.SaveChangesAsync();
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, $"Error agregando un País {aCountryName}");
      return Tuple.Create(500, (CountryDTO?) null);
    }
    return Tuple.Create(200,CountryDTO.FromDbItem(dbCountry))!;
  }
  
  public static IPAddress? GetRemoteHostIpAddressUsingRemoteIpAddress(HttpContext httpContext)
  {
    return httpContext.Connection.RemoteIpAddress;
  }

  public static IPAddress? GetRemoteHostIpAddressUsingXForwardedFor(HttpContext httpContext)
  {
    IPAddress? remoteIpAddress = null;
    var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
    
    if (string.IsNullOrEmpty(forwardedFor)) return remoteIpAddress;
    
    var ips = forwardedFor
      .Split(',', StringSplitOptions.RemoveEmptyEntries)
      .Select(s => s.Trim());
    
    foreach (var ip in ips)
    {
      if (!IPAddress.TryParse(ip, out var address) || address.AddressFamily is not (AddressFamily.InterNetwork or AddressFamily.InterNetworkV6)) continue;
      remoteIpAddress = address;
      break;
    }

    return remoteIpAddress;
  }

  public static IPAddress? GetRemoteHostIpAddressUsingXRealIp(HttpContext httpContext)
  {
    IPAddress? remoteIpAddress = null;
    
    var xRealIpExists = httpContext.Request.Headers.TryGetValue("X-Real-IP", out var xRealIp);
    
    if (!xRealIpExists) return remoteIpAddress;
    
    if (!IPAddress.TryParse(xRealIp, out IPAddress? address))
    {
      return remoteIpAddress;
    }

    return address.AddressFamily is AddressFamily.InterNetwork or AddressFamily.InterNetworkV6 ? address : remoteIpAddress;
  }
}