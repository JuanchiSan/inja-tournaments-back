using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace InjaAdmin.Auth;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
  private readonly ProtectedSessionStorage _sessionStorage;
  private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
  
  public CustomAuthStateProvider(ProtectedSessionStorage sessionStorage)
  {
    _sessionStorage = sessionStorage;
  }
  
  public override async Task<AuthenticationState> GetAuthenticationStateAsync()
  {
    try
    {
      var userSessionStorageResult = await _sessionStorage.GetAsync<UserSession>(Helper.paramUserSession);
      var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

      if (userSession == null)
        return await Task.FromResult(new AuthenticationState(_anonymous));
    
      var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
      {
        new Claim(ClaimTypes.Name, userSession.Name),
        new Claim(ClaimTypes.Role, userSession.Role)
      }, "CustomAuth"));

      return await Task.FromResult(new AuthenticationState(claimsPrincipal));
    }
    catch (Exception e)
    {
      Serilog.Log.Error(e, "Error tratando de Autentificar");
      return await Task.FromResult(new AuthenticationState(_anonymous));
    }
  }

  public async Task UpdateAuthenticationState(UserSession? userSession)
  {
    ClaimsPrincipal claimsPrincipal;

    if (userSession != null)
    {
      await _sessionStorage.SetAsync(Helper.paramUserSession, userSession);
      claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
      {
        new(ClaimTypes.Name, userSession.Name),
        new(ClaimTypes.Role, userSession.Role)
      }));
    }
    else
    {
      await _sessionStorage.DeleteAsync(Helper.paramUserSession);
      claimsPrincipal = _anonymous;
    }

    NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
  }
}