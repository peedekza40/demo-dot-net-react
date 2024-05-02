using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PracticeReactApp.Server.Infrastructures.Services.Interfaces;

namespace PracticeReactApp.Server.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeForbiddenAttribute : Attribute, IAuthorizationFilter
    {
        public string? Path { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            var accountService = context.HttpContext.RequestServices.GetService<IAccountService>();
            if(accountService.GetCurrentUser() == null)
            {
                // not logged in
                context.Result = new UnauthorizedObjectResult("Unauthorized");
                return;
            }

            if(accountService.CurrentUserIsHavePermissionEndpoint(context.HttpContext, Path) == false)
            {
                // role not have permission
                context.Result = new ForbidResult("Forbidden");
                return;
            }
        }
    }
}