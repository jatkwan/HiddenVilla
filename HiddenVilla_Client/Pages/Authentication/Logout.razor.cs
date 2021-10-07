using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
