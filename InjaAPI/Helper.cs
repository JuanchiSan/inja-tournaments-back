using System.Net;
using System.Net.Sockets;
using System.Security.Policy;

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