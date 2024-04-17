using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PracticeReactApp.Server.Constants;
using PracticeReactApp.Server.Data;
using PracticeReactApp.Server.Models;
using PracticeReactApp.Server.ViewModels;
using System.Security.Claims;

namespace PracticeReactApp.Server.Controllers
{
    [Authorize( Roles = Roles.Admin )]
    [ApiController]
    [Route("[controller]")]
    public class RoleManagementController : ControllerBase
    {
        public IActionResult Search()
        {
            return Ok();
        }
    }
}