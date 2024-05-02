using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeReactApp.Server.Constants;
using PracticeReactApp.Server.Filters;
using PracticeReactApp.Server.Models.Entities;
using PracticeReactApp.Server.ViewModels.Account;

namespace PracticeReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeForbidden( Path = "Account" )]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("GetCurrentUserProfile")]
        public IActionResult GetCurrentUserProfile()
        {
            var currentUser = userManager.GetUserAsync(User).Result;
            return Ok(new 
            {
                UserName = currentUser?.UserName,
                Email = currentUser?.Email,
                FirstName = currentUser?.FirstName,
                LastName = currentUser?.LastName
            });
        }

        [HttpGet]
        [Route("IsLogin")]
        // [AuthorizeForbidden( Path = "Account/IsLogin" )]
        public IActionResult IsLogin()
        {
            return Ok(true);
        }

        [HttpGet]
        [Route("IsHavePermission")]
        public IActionResult IsHavePermission(string path)
        {
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("IsExistsEmail")]
        public IActionResult IsExistsEmail([FromBody] string email)
        {
            var result =  userManager.FindByEmailAsync(email).Result;
            return Ok(result != null);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            model.UserName = model.Email;
            var result = await userManager.CreateAsync(model, model.Password ?? string.Empty);
            if(result.Succeeded == false)
            {
                return BadRequest(result);
            }
            
            var user = userManager.Users.FirstOrDefault(x => x.UserName == model.UserName);
            var resultAddRole = await userManager.AddToRoleAsync(user, Roles.User);
            if(resultAddRole.Succeeded == false)
            {
                return BadRequest(resultAddRole);
            }

            return Ok(result);
        }

        [HttpPost]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
            return Ok();
        }

        private readonly UserManager<User> userManager;
        private readonly RoleManager<Role> roleManager;

        public AccountController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
    }
}