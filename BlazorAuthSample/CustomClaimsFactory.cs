using BlazorAuthSample.Models;
using BlazorAuthSample.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Tasks;


namespace BlazorAuthSample
{
    public class CustomClaimsFactory
    : AccountClaimsPrincipalFactory<RemoteUserAccount>
    {
  

        public CustomClaimsFactory(NavigationManager navigationManager,
            IAccessTokenProviderAccessor accessor) : base(accessor)
        {
        
        }

        public async override ValueTask<ClaimsPrincipal> CreateUserAsync(
            RemoteUserAccount account, RemoteAuthenticationUserOptions options)
        {
            Console.WriteLine("Running CustomClaimsFactory.CreateUserAsync");
            var initialUser = await base.CreateUserAsync(account, options);
            if (initialUser.Identity.IsAuthenticated)
            {
                string _userName = UserUtil.GetUsername(initialUser);
                //    var client = this._clientFactory.CreateClient("UCEmail.ServerAPI.NoAuthenticationClient");
                //    var users = await client.GetFromJsonAsync<List<User>>($"UCEmail/GetUserRoles/{userName}");

                // mock user

                var users = new List<User>
                {
                    new User
                    {
                        Id = 1,
                        UserName = _userName,
                        RoleName = "Admin,Role1,Role2,Role3"
                    }
                };

                foreach (var u in users)
                {
                    var roles = u.RoleName.Split(',');
                    foreach (var role in roles)
                    {
                        ((ClaimsIdentity)initialUser.Identity)
                            .AddClaim(new Claim(ClaimTypes.Role, role.Trim()));
                    }
                }
            }

            return initialUser;
        }
    }
}
