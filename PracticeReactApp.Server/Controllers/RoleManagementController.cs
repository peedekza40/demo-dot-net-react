using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Core.Models;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Filters;
using PracticeReactApp.Server.ViewModels;
using PracticeReactApp.Server.ViewModels.RoleManagement;
using System.Data;

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
            return Ok(new ApiResponseViewModel<DataTableResponseModel<Role>?>() { Data = result });
        }

        [HttpPost]
        [Route("GetById")]
        public IActionResult GetById([FromBody] string id)
        { 
            var role = roleRepository.GetById(id);
            if(role == null)
            {
                Ok(new ApiResponseViewModel<Role?>() { 
                    IsSuccess = false,
                    ErrorMessage = "Role not found."
                });
            }

            var model = new MaintenanceRoleViewModel
            {
                Id = role.Id,
                Name = role.Name,
                NormalizedName = role.NormalizedName
            };

            return Ok(new ApiResponseViewModel<MaintenanceRoleViewModel>() { Data = model });
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