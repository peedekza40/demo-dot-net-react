using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Infrastructures.Services.Interfaces
{
    public interface IAccountService
    {
        User? GetCurrentUser();

        bool CurrentUserIsHavePermissionEndpoint(string? path);

        bool IsHavePermissionEndpoint(User user, string? path);
    }
}