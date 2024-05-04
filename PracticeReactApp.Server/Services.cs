using PracticeReactApp.Infrastructures.Repositories;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Infrastructures.Services;
using PracticeReactApp.Infrastructures.Services.Interfaces;

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