using ItemShop.Data.Models;
using ItemShop.Features.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ItemShop.Features.Identity
{
    public class IdentityController : ApiController
    {
        private readonly UserManager<User> _userManager;
        private readonly AppSettings _appSettings;

        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        
        [HttpPost]
        [Route((nameof(Login)))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if(user == null)
            {
                return Unauthorized();
            }
            var validPassword = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!validPassword)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { 
                    new Claim(ClaimTypes.NameIdentifier, user.Id), 
                    new Claim(ClaimTypes.Name, user.UserName) 
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);
            return new LoginResponseModel
            {
                Token = encryptedToken,
                Username = user.UserName
            };
        }

        [HttpPost]
        [Route((nameof(Register)))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
            };
            var results = await _userManager.CreateAsync(user, model.Password);
            if(results.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

         
    }
}
