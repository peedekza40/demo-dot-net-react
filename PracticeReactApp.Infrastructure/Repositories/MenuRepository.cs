using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Core.Data;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;

namespace PracticeReactApp.Infrastructures.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        public List<Menu> GetByCode(string? code)
        {
            return context.Menus
                    .Include(x => x.RoleMenus)
                    .ThenInclude(x => x.Role)
                    .Where(x => x.Code == code)
                    .ToList();
        }

        public List<Menu> GetByRoles(List<Role> roles)
        {
            var roleIds = roles.Select(x => x.Id).ToList() ?? new List<string>();
            return context.RoleMenus
                    .Where(x => roleIds.Contains(x.RoleId))
                    .Include(x => x.MenuCodeNavigation)
                    .Select(x => x.MenuCodeNavigation)
                    .OrderBy(x => x.Order)
                    .ToList();
        }

        private readonly PracticeDotnetReactContext context;

        public MenuRepository(PracticeDotnetReactContext context)
        {
            this.context = context;
        }
    }
}