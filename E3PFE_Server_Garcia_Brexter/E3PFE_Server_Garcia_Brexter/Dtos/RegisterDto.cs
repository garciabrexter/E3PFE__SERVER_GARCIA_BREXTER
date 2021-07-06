using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos
{
    // creating data transfer objects register
    public class RegisterDto
    {
        public string Name { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string Section { set; get; }
        public string Hobby { set; get; }
    }
}
