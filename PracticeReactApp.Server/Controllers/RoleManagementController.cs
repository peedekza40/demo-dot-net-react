using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeReactApp.Server.Constants;
using PracticeReactApp.Server.Infrastructures.Extensions;
using PracticeReactApp.Server.Models;

namespace PracticeReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize( Roles = Roles.Admin )]
    public class RoleManagementController : ControllerBase
    {
        [HttpPost]
        [Route("Search")]
        public IActionResult Search([FromBody] DataTableRequestModel dataTableRequest)
        {
            var result = _roleManager.Roles.ToDataTablesResponse(dataTableRequest);
            var jsonData = JsonConvert.SerializeObject(result);
            return Ok(jsonData);
        }

        // [HttpPost]
        // [Route("SaveRole")]
        // public IActionResult SaveRole([FromBody]MaintenanceRoleViewModel model)
        // {
        //     var saveModel = new IdentityRole(model.Name ?? string.Empty);
        //     if (model.Mode == ActionMode.Add)
        //     {
        //         _roleManager.CreateAsync(saveModel).Wait();
        //     }
        //     else
        //     {
        //         saveModel = _roleManager.Roles.FirstOrDefault(r => r.Id == model.Id);
        //         if (saveModel != null)
        //         {
        //             _roleManager.UpdateAsync(saveModel).Wait();
        //         }
        //     }

        //     return Ok();
        // }

        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleManagementController(
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
    }
}