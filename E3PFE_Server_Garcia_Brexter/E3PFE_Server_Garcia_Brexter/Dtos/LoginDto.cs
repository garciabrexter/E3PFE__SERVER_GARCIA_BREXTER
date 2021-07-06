using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    // creating the data transfer objects login
    public class LoginDto
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
