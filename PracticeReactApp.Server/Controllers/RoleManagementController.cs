using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Core.Models;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Filters;
using PracticeReactApp.Server.ViewModels.RoleManagement;

namespace PracticeReactApp.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AuthorizeForbidden(Path = "RoleManagement")]
    public class RoleManagementController : ControllerBase
    {
        [HttpPost]
        [Route("Search")]
        public IActionResult Search([FromBody] DataTableRequestModel dataTableRequest)
        {
            var result = roleRepository.GetDataTableResponse(dataTableRequest);
            var jsonData = JsonConvert.SerializeObject(result);
            return Ok(jsonData);
        }

        [HttpPost]
        [Route("IsExists")]
        public IActionResult IsExists([FromBody] string id)
        {
            return Ok(roleRepository.IsExists(id));
        }

        [HttpPost]
        [Route("SaveRole")]
        public IActionResult SaveRole([FromBody] MaintenanceRoleViewModel model)
        {
            if (ActionMode.Add == model.Mode)
            {
                roleRepository.Insert(model);
            }
            else if (ActionMode.Edit == model.Mode)
            {
                roleRepository.Update(model);
            }

            return Ok();
        }

        private readonly RoleManager<Role> roleManager;
        private readonly IRoleRepository roleRepository;

        public RoleManagementController(
            RoleManager<Role> roleManager,
            IRoleRepository roleRepository)
        {
            this.roleManager = roleManager;
            this.roleRepository = roleRepository;
        }
    }
}