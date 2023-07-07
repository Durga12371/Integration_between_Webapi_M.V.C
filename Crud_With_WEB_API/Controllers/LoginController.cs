using Crud_With_WEB_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Crud_With_WEB_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration config;
        public LoginController(IConfiguration configuration)
        {

            config = configuration;

        }
        private bool Autheticateuser(Users user)
        {
            bool _user = false;
            if (user.Username == "admin" && user.Password == "admin@123")
            {
                _user = true;

            }
            if(user.Username == "user" && user.Password == "user@123")
            {
                _user = true;

            };
            return _user;
        }
        private string GenerateToken(Users user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(config["Jwt:Issuer"], config["Jwt:Audience"], null,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials


                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users user)
        {
            IActionResult response = Unauthorized();
            var user_ = Autheticateuser(user);
            if (user_)
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token });

            }
            else
            {
                response = BadRequest();
            }
            return response;

        }

    }
}


