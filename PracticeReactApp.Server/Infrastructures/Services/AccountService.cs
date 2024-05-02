using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PracticeReactApp.Server.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Infrastructures.Services.Interfaces;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Infrastructures.Services
{
    public class AccountSerivce : IAccountService
    {
        public User? GetCurrentUser()
        {
            var userContext = httpContextAccessor?.HttpContext?.User;
            return userContext != null ? userManager.GetUserAsync(userContext).Result : null;
        }

        public bool CurrentUserIsHavePermissionEndpoint(HttpContext httpContext, string? path)
        {
            var currentUser = GetCurrentUser();
            return currentUser != null ? IsHavePermissionEndpoint(currentUser, path) : false;
        }

        public bool IsHavePermissionEndpoint(User user, string? path)
        {
            var roles = roleRepository.GetUserRoles(user);
            var endpoints = new List<Apiendpoint>();
            foreach (var role in roles)
            {
                endpoints = endpoints.Union(role.RoleApiendpoints.Select(x => x.ApicodeNavigation)).ToList();
            }

            return endpoints.Any(x => x.Path == path && x.IsActive == true);
        }

        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRoleRepository roleRepository;

        public AccountSerivce(
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IRoleRepository roleRepository)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.roleRepository = roleRepository;
        }
    }
}