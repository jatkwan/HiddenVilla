using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Register
    {
        private UserRequestDTO userForRegisteration = new UserRequestDTO();

        public bool IsProcessing { get; set; } = false;

        public bool ShowRegisterationErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task RegisterUser()
        {
            ShowRegisterationErrors = false;
            IsProcessing = true;
            var result = await AuthenticationService.RegisterUser(userForRegisteration);
            if (result.IsRegisterationSucessful)
            {
                IsProcessing = false;
                NavigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegisterationErrors = true;
            }
        }
    }
}
