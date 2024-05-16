using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Infrastructures.Services.Interfaces;

namespace PracticeReactApp.Infrastructures.Services
{
    public class AccountSerivce : IAccountService
    {
        public User? GetCurrentUser()
        {
            var userContext = httpContextAccessor?.HttpContext?.User;
            return userContext != null ? userManager.GetUserAsync(userContext).Result : null;
        }

        public List<Menu> GetCurrentUserMenus()
        {
            var currentUser = GetCurrentUser() ?? new User();
            var roles = roleRepository.GetUserRoles(currentUser);
            return menuRepository.GetByRoles(roles)
                    .Where(x => x.IsActive == true && x.IsDisplay == true)
                    .ToList();
        }

        public bool CurrentUserIsHavePermissionEndpoint(string? path)
        {
            var currentUser = GetCurrentUser();
            return currentUser != null ? IsHavePermissionEndpoint(currentUser, path) : false;
        }

        public bool CurrentUserIsHavePermissionPage(string? path)
        {
            var currentUser = GetCurrentUser();
            return currentUser != null ? IsHavePermissionPage(currentUser, path) : false;
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

        public bool IsHavePermissionPage(User user, string? path)
        {
            var roles = roleRepository.GetUserRoles(user);
            var menus = menuRepository.GetByRoles(roles);
            return menus.Any(x => x.IsActive == true) || path == Base.HomePagePath;
        }

        private readonly UserManager<User> userManager;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IRoleRepository roleRepository;
        private readonly IMenuRepository menuRepository;

        public AccountSerivce(
            UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor,
            IRoleRepository roleRepository,
            IMenuRepository menuRepository)
        {
            this.userManager = userManager;
            this.httpContextAccessor = httpContextAccessor;
            this.roleRepository = roleRepository;
            this.menuRepository = menuRepository;
        }
    }
}