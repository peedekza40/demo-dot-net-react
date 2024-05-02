using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Infrastructures.Services.Interfaces
{
    public interface IAccountService
    {
        User? GetCurrentUser();
        
        bool CurrentUserIsHavePermissionEndpoint(HttpContext httpContext, string? path);
        
        bool IsHavePermissionEndpoint(User user, string? path);
    }
}