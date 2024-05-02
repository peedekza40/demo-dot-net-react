using PracticeReactApp.Server.Infrastructures.Repositories;
using PracticeReactApp.Server.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Infrastructures.Services;
using PracticeReactApp.Server.Infrastructures.Services.Interfaces;

namespace PracticeReactApp.Server
{
    public static class Services
    {
        public static void ConfigureServices(IServiceCollection service)
        {
            //repositories
            service.AddTransient<IMenuRepository, MenuRepository>();
            service.AddTransient<IRoleRepository, RoleRepository>();

            //services
            service.AddTransient<IAccountService, AccountSerivce>();
        }
    }
}