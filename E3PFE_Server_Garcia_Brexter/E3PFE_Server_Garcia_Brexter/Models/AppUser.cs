using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Api.Models
{
    public class AppUser // creating user
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Section { get; set; }
        public string Hobby { get; set; }
        [JsonIgnore]public string Password { get; set; }
    }
}
