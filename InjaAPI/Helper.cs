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

}
