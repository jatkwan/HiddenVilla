using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace HiddenVilla_Client.Service.IService
{
    public interface IAuthenticationService
    {
        public Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO userForRegisteration);

        public Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication);

        public Task Logout();
    }
}