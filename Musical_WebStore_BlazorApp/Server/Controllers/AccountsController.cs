using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Musical_WebStore_BlazorApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;

        public AccountsController(UserManager<User> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(ProfileModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            user.UserName = model.Login;
            await _userManager.ChangePasswordAsync(user, model.OldPassword, model.Password);
            await _userManager.UpdateAsync(user);
            return Ok(new LoginResult { Successful = true });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegisterModel model)
        {
            var newUser = new User
            {
                UserName = model.Username,
                Email = model.Email
            };

            if (await EmailAlreadyInUse(model.Email))
            {
                var value = new RegisterResult 
                { 
                    Successful = false, Errors = new[] { $"{model.Email} address is already in use" } 
                };

                return Ok(value);
            }

            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return Ok(new RegisterResult { Successful = false, Errors = errors });
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            var url = Url.Action(
                "Confirm",
                "EmailConfirmation",
                new { userId = newUser.Id, token },
                protocol: HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync(newUser.Email, "Password confirmation",
                "Confirm your password by visiting the following link: " + url
                );

            return Ok(new RegisterResult { Successful = true });
        }

        private async Task<bool> EmailAlreadyInUse(string email)
        {
            var any = await _userManager.Users.AnyAsync(i => i.Email == email);

            return any;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class EmailConfirmationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public EmailConfirmationController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> Confirm(string userId, string token)
        {
            var succeeded = true;

            if (String.IsNullOrWhiteSpace(userId) || String.IsNullOrWhiteSpace(token))
            {
                succeeded = false;
            }

            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                succeeded = false;
            }

            if (succeeded)
            {
                var result = await userManager.ConfirmEmailAsync(user, token);

                succeeded = result.Succeeded;
            }

            if (succeeded)
            {
   
                return Redirect("/login");
            }
            else
            {
                return Redirect("/confirmationError");
            }
        }
    }
}
