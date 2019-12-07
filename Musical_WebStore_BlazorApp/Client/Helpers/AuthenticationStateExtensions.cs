using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;

namespace Musical_WebStore_BlazorApp.Client.Helpers
{
    public static class AuthenticationStateHelper
    {
        public static AuthenticationState GetEmpty()
            => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

        public static AuthenticationState GetWithClaimsIdentity(IEnumerable<Claim> claims, string authType)
            => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, authType)));
    }
}
