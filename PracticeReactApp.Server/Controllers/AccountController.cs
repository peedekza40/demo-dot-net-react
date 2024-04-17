using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeReactApp.Server.Constants;
using PracticeReactApp.Server.Data;
using PracticeReactApp.Server.Models;
using PracticeReactApp.Server.ViewModels;
using PracticeReactApp.Server.ViewModels.Account;
using System.Security.Claims;

namespace PracticeReactApp.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        [Route("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet]
        [Route("GetCurrentUserProfile")]
        public IActionResult GetCurrentUserProfile()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
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
            var result =  _userManager.FindByEmailAsync(email).Result;
            return Ok(result != null);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            model.UserName = model.Email;
            var result = await _userManager.CreateAsync(model, model.Password ?? string.Empty);
            if(result.Succeeded == false)
            {
                return BadRequest(result);
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

        [HttpPost]
        [AllowAnonymous]
        [Route("SaveRole")]
        public IActionResult SaveRole([FromBody]MaintenanceRoleViewModel model)
        {
            var saveModel = new IdentityRole(model.Name ?? string.Empty);
            if (model.Mode == ActionMode.Add)
            {
                _roleManager.CreateAsync(saveModel).Wait();
            }
            else
            {
                saveModel = _roleManager.Roles.FirstOrDefault(r => r.Id == model.Id);
                if (saveModel != null)
                {
                    _roleManager.UpdateAsync(saveModel).Wait();
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("SetupRole")]
        public async Task<IActionResult> SetupRole()
        {
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new IdentityRole("Admin");
                await _roleManager.CreateAsync(adminRole);

                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "projects.view"));
                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "projects.create"));
                await _roleManager.AddClaimAsync(adminRole, new Claim(CustomClaimTypes.Permission, "projects.update"));
            }

            return Ok();
        }

        private readonly PracticeReactContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            PracticeReactContext context,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}