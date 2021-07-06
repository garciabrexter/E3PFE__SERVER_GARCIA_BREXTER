using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

namespace Api.Helpers
{
    // service for the jwt
    public class JwtService
    {
        private string securitykey = "this is a very secure key"; // a key that will be used for encryption
        public string Generate(int id)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securitykey)); // getting a security symmetric key using an enconded default secured key
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature); // specifying the symmetric key and the algorithm for encryption
            var header = new JwtHeader(credentials); // using the credentials for the header of the jwt

            var payload = new JwtPayload(id.ToString(), null, null, null, DateTime.Today.AddDays(1)); // passing the issuer and expiration to the payload

            var securityToken = new JwtSecurityToken(header, payload); // the token by combining the header and the payload

            return (new JwtSecurityTokenHandler().WriteToken(securityToken)); // returning the security token
        }

        public JwtSecurityToken Verify(string jwt) // used for verifying if the credentials are correct
        {
            var tokenHandler = new JwtSecurityTokenHandler(); // creating a new jwtsecurity handler
            var key = Encoding.ASCII.GetBytes(securitykey); // encoding the secured key to ASCII
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters // validating the token
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            },
                out SecurityToken validatedToken);

            return (JwtSecurityToken)validatedToken;
        }
    }
}
