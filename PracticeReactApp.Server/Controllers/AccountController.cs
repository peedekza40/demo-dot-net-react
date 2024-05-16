using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Infrastructures.Services.Interfaces;
using PracticeReactApp.Server.Filters;
using PracticeReactApp.Server.ViewModels;
using PracticeReactApp.Server.ViewModels.Account;

namespace PracticeReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeForbidden(Path = "Account")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("GetCurrentUserProfile")]
        public IActionResult GetCurrentUserProfile()
        {
            var currentUser = accountSerivce.GetCurrentUser();
            return Ok(currentUser);
        }

        [HttpGet]
        [Route("GetCurrentUserMenus")]
        public IActionResult GetCurrentUserMenus()
        {
            var menus = accountSerivce.GetCurrentUserMenus();
            return Ok(new ApiResponseViewModel<List<Menu>?>() { Data = menus });
        }

        [HttpPost]
        [Route("IsHavePermission")]
        public IActionResult IsHavePermission([FromBody] string path)
        {
            var result = accountSerivce.CurrentUserIsHavePermissionPage(path);
            if(result == false)
            {
                return Forbid();
            }

            return Ok(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("IsExistsEmail")]
        public IActionResult IsExistsEmail([FromBody] string email)
        {
            var result = userManager.FindByEmailAsync(email).Result;
            return Ok(result != null);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            model.UserName = model.Email;
            var result = await userManager.CreateAsync(model, model.Password ?? string.Empty);
            if (result.Succeeded == false)
            {
                return BadRequest(result);
            }

            var user = userManager.Users.FirstOrDefault(x => x.UserName == model.UserName);
            var resultAddRole = await userManager.AddToRoleAsync(user, RoleCode.User);
            if (resultAddRole.Succeeded == false)
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
        private readonly IAccountService accountSerivce;

        public AccountController(
            UserManager<User> userManager,
            IAccountService accountSerivce)
        {
            this.userManager = userManager;
            this.accountSerivce = accountSerivce;
        }
    }
}