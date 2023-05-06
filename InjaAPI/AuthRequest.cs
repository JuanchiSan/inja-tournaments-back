namespace InjaAPI;

public class AuthRequest
{
  public string Email { get; set; } = null!;
  public string Password { get; set; } = null!;
  public int EventId { get; set; } = 0!;
}