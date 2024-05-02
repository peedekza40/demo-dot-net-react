using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Server.Constants;
using PracticeReactApp.Server.Data;
using PracticeReactApp.Server.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Infrastructures.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public List<Role> GetUserRoles(User user)
        {
            var roles = userManager.GetRolesAsync(user).Result;
            return context.Roles
                    .Where(x => roles.Contains(x.Name))
                    .Include(x => x.RoleApiendpoints).ThenInclude(x => x.ApicodeNavigation)
                    .ToList() ?? new List<Role>();
        }

        private readonly UserManager<User> userManager;
        private readonly PracticeDotnetReactContext context;

        public RoleRepository(
            UserManager<User> userManager,
            PracticeDotnetReactContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }
    }
}