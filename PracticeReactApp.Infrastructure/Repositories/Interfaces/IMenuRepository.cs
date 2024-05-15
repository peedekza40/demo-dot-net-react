using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Infrastructures.Repositories.Interfaces
{
    public interface IMenuRepository
    {
        List<Menu> GetByCode(string? code);

        List<Menu> GetByRoles(List<Role> roles);
    }
}