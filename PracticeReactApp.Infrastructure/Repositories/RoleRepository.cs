using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Core.Data;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Core.Extensions;
using PracticeReactApp.Core.Models;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;

namespace PracticeReactApp.Infrastructures.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public DataTableResponseModel<Role> GetDataTableResponse(DataTableRequestModel dataTableRequest)
        {
            return roleManager.Roles
                .OrderBy(r => r.Id)
                .ToDataTablesResponse(dataTableRequest);
        }

        public Role? GetById(string id)
        {
            return roleManager.Roles.FirstOrDefault(x => x.Id == id);
        }

        public List<Role> GetUserRoles(User user)
        {
            var roles = userManager.GetRolesAsync(user).Result;
            return context.Roles
                    .Where(x => roles.Contains(x.Name))
                    .Include(x => x.RoleApiendpoints).ThenInclude(x => x.ApicodeNavigation)
                    .ToList() ?? new List<Role>();
        }

        public bool IsExists(string id)
        {
            return roleManager.Roles.Any(x => x.Id == id);
        }

        public void Insert(Role model)
        {
            roleManager.CreateAsync(model).Wait();
        }

        public void Update(Role model)
        {
            var updateModel = roleManager.Roles.FirstOrDefault(x => x.Id == model.Id);
            if (updateModel != null)
            {
                updateModel.Id = model.Id;
                updateModel.Name = model.Name;
                roleManager.UpdateAsync(updateModel).Wait();
            }
        }

        private readonly RoleManager<Role> roleManager;
        private readonly UserManager<User> userManager;
        private readonly PracticeDotnetReactContext context;

        public RoleRepository(
            RoleManager<Role> roleManager,
            UserManager<User> userManager,
            PracticeDotnetReactContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.context = context;
        }
    }
}