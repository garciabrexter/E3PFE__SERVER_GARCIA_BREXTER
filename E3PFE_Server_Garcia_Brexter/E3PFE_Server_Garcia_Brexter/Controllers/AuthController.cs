using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Helpers;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route(template:"api")] // routing of api
    [ApiController] // differentiating api from mvc
    public class AuthController : ControllerBase // adding repository and jwtservice
    {
        private readonly IUserRepository _repository;  // connecting the repository for authentication
        private readonly JwtService _jwtService; //connectiong the helper made to process jwt

        public AuthController(IUserRepository repository, JwtService jwtService)
        {
            _repository = repository;
            _jwtService = jwtService;
        }

        [HttpPost(template: "register")] // post request
        public IActionResult Register(RegisterDto dto) // creating a user using user repository
        {
            var user = new AppUser
            {
                Name = dto.Name,
                Email = dto.Email,
                Section = dto.Section,
                Hobby = dto.Hobby,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) // encrypting password
            };

            return Created("success", _repository.Create(user)); // success message by returning created user
        }

        [HttpPost(template:"login")] // post request
        public IActionResult Login(LoginDto dto) // verifying and logging in user
        {
            var user = _repository.GetByEmail(dto.Email); // getting user email

            if (user == null) return BadRequest(new { message = "Invalid Credentials" }); // checking if user is null

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password)) // checking if the encrypted password matches the hash in the database
            {
                return BadRequest(new { message = "Invalid Credentials" }); // error message if not matched
            }

            var jwt = _jwtService.Generate(user.Id); // generating jwt token

            Response.Cookies.Append("jwt", jwt, new CookieOptions // adding the jwt token to the cookies
            {
                HttpOnly = true,
            });

            return Ok(new // success message
            { 
                message = "success"
            });
        }

        [HttpGet(template:"user")] // finding user
        public IActionResult GetUser()
        {
            try
            {
                var jwt = Request.Cookies["jwt"]; // request cookies for jwt
                var token = _jwtService.Verify(jwt); // verify if the token matches

                int userId = int.Parse(token.Issuer); // getting the id from issuer

                var user = _repository.GetById(userId); // getting user's id

                return Ok(user);
            }
            catch(Exception e) // catching error
            {
                return Unauthorized();
            }
            
        }

        [HttpPost(template:"logout")] // removing the token from the cookie
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok(new // success message
            {
                message = "success"
            });
        }
    }
}
