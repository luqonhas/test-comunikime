using ComunikiMe.Domain.Commands.Authentications;
using ComunikiMe.Domain.Entities;
using ComunikiMe.Domain.Handlers.Authentications;
using ComunikiMe.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Api.Controllers
{
    [Route("v1/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [Route("signin/email")]
        [HttpPost]
        public GenericCommandResult SignInEmail(LoginEmailCommand command, [FromServices] LoginEmailHandle handle)
        {
            var result = (GenericCommandResult)handle.Handler(command);

            if (result.SuccessFailure)
            {
                var token = GenerateJSONWebToken((User)result.Data);

                return new GenericCommandResult(true, "User successfully logged in!", new { token = token });
            }

            return new GenericCommandResult(false, result.Message, result.Data);
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("comunikime-authentication-key"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.FamilyName, userInfo.UserName),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(ClaimTypes.Role, userInfo.Permission.ToString()),
            new Claim("role", userInfo.Permission.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
            };

            var token = new JwtSecurityToken(
                "comunikime",
                "comunikime",
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
