using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Infrastructures.Services.Interfaces
{
    public interface IAccountService
    {
        User? GetCurrentUser();

        List<Menu> GetCurrentUserMenus();

        bool CurrentUserIsHavePermissionEndpoint(string? path);

        bool CurrentUserIsHavePermissionPage(string? path);

        bool IsHavePermissionEndpoint(User user, string? path);

        bool IsHavePermissionPage(User user, string? path);
    }
}