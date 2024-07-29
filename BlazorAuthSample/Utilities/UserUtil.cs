using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;


namespace BlazorAuthSample.Utilities
{
    public class UserUtil
    {
        public static string GetUsername(ClaimsPrincipal user)
        {
            var userEmail = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value;
            if (String.IsNullOrWhiteSpace(userEmail))
            {
                userEmail = user.FindFirst(c => c.Type == "preferred_username")?.Value;
            }
            return userEmail;
        }

        public static string GetUsername(AuthenticationState authState)
        {
            var user = authState.User;
            return GetUsername(user);
        }

        public static List<Claim> GetUserRoles(ClaimsPrincipal user)
        {
            return user.FindAll(c => c.Type == ClaimTypes.Role).ToList();
        }

        public static List<Claim> GetUserRoles(AuthenticationState authState)
        {
            return GetUserRoles(authState.User);
        }

    }
}
 