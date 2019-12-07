using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Musical_WebStore_BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public LoginController(IConfiguration configuration,
                               SignInManager<User> signInManager,
                               UserManager<User> userManager)
        {
            _configuration = configuration;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null)
            {
                return UserIsNullError();
            }

            if (!user.EmailConfirmed)
            {
                return EmailNotConfirmed();
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, login.Password, false, false);

            if (!result.Succeeded)
            {
                return Ok(new LoginResult { Successful = false, Error = "Username and password are invalid." });
            }

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, login.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );

            return Ok(new LoginResult { Successful = true, Token = new JwtSecurityTokenHandler().WriteToken(token) });
        }

        private IActionResult UserIsNullError()
        {
            return Ok(new LoginResult
            {
                Successful = false,
                Error = "This email address is not registered in the system"
            });
        }

        private IActionResult EmailNotConfirmed() 
        {
            return Ok(new LoginResult
            {
                Successful = false,
                Error = "Please, confirm your email following the link on your email box"
            }); 
        }
    }
}
