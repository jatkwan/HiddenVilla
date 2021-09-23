using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AuthenticationResponseDTO
    {
        public bool IsAuthSucessful { get; set; }
        public string ErrorsMessage { get; set; }
        public string Token { get; set; }
        public UserDTO userDTO { get; set; }
    }
}
