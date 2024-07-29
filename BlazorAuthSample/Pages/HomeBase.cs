using BlazorAuthSample.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Security.Claims;

namespace BlazorAuthSample.Pages
{

    [Authorize(Roles = "Admin,Role1,Role2")]
    public class HomeBase : ComponentBase, IDisposable
    {
 
        private const string _defaultLoadingText = "Loading...";
        private bool _eventHandlersAdded = false;
        private bool _loadedUserInfo = false;
        private string _username;
        private List<Claim> _userRoles;

        private string _dialogOpen = string.Empty;

        [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; }

        protected async override Task OnParametersSetAsync()
        {
            if (!_loadedUserInfo)
            {
                var authState = await AuthState;
                _username = UserUtil.GetUsername(authState);
                _userRoles = UserUtil.GetUserRoles(authState);
               
                _loadedUserInfo = true;
            }
 

            await base.OnParametersSetAsync();
        }
 

        public void Dispose()
        {
      
        }
    }
}
    