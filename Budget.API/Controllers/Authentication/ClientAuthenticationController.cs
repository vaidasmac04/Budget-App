using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Budget.Application.DTOs;
using Budget.Domain;
using Budget.API.Helpers;
using Budget.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Budget.API.Controllers.Authentication
{
    [Authorize]
    [Route("api/Authentication")]
    [ApiController]
    public class ClientAuthenticationController : ControllerBase
    {
        private readonly IClientAuthentication _clientAuthentication;
        private readonly AppSettings _appSettings;

        public ClientAuthenticationController(
            IClientAuthentication clientAuthentication,
            IOptions<AppSettings> appSettings)
        {
            _clientAuthentication = clientAuthentication;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDTO model)
        {
            var client = _clientAuthentication.Authenticate(model.Username, model.Password);

            if (client == null)
                return Unauthorized(new { message = "Username or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, client.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                client.Id,
                client.Username,
                client.FirstName,
                client.LastName,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterDTO model)
        {
            Client client = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Username = model.Username
            };

            try
            {
                _clientAuthentication.Create(client, model.Password);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
