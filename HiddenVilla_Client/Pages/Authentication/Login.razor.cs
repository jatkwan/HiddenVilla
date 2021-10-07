using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO userForAuthentication = new AuthenticationDTO();

        public bool IsProcessing { get; set; } = false;

        public bool ShowRegisterationErrors { get; set; }

        public string Errors { get; set; }

        public string ReturnUrl { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task LoginUser()
        {
            ShowRegisterationErrors = false;
            IsProcessing = true;
            var result = await AuthenticationService.Login(userForAuthentication);
            if (result.IsAuthSucessful)
            {
                IsProcessing = false;

                var absoluteUri = new Uri(NavigationManager.Uri);
                var querryParam = HttpUtility.ParseQueryString(absoluteUri.Query);
                ReturnUrl = querryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    NavigationManager.NavigateTo("/" + ReturnUrl);
                }

            }
            else
            {
                IsProcessing = false;
                Errors = result.ErrorsMessage;
                ShowRegisterationErrors = true;
            }
        }
    }
}
