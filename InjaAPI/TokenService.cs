using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace InjaAPI
{
	public class TokenService
	{
		private const int ExpirationMinutes = 30;

		public string CreateToken(InjaData.Models.Injauser user, int eventId)
		{
			var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
			var token = CreateJwtToken(
					CreateClaims(user, eventId),
					CreateSigningCredentials(),
					expiration
			);
			var tokenHandler = new JwtSecurityTokenHandler();
			return tokenHandler.WriteToken(token);
		}

		private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
				DateTime expiration) =>
				new(
						Helper.JWTIssuer,
						Helper.JWTAudience,
						claims,
						expires: expiration,
						signingCredentials: credentials
				);

		private List<Claim> CreateClaims(InjaData.Models.Injauser user, int eventId)
		{
			try
			{
				var roles = new InjaData.Models.dbContext().Injauserusertypes.Where(x => x.Userid == user.Id && x.Eventid == eventId).Select(y => y.Typeid.ToString()).ToList();
				var claims = new List<Claim>
								{
										new Claim(JwtRegisteredClaimNames.Sub, "InjaToken"),
										new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
										new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
										new Claim(Helper.JWTClaimId, user.Id.ToString()),
										new Claim(Helper.JWTClaimUserName, $"{user.Lastname}, {user.Firstname}"),
										new Claim(Helper.JWTClaimEmail, user.Mail),
										new Claim(Helper.JWTClaimRoles, string.Join(",", roles))
								};
				return claims;
			}
			catch (Exception e)
			{
				Serilog.Log.Error("Error Generating JWT Token", e);
				throw;
			}
		}

		private SigningCredentials CreateSigningCredentials()
		{
			return new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Helper.TokenSecret)), SecurityAlgorithms.HmacSha256);
		}
	}
}
