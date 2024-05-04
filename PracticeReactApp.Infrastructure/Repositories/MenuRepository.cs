using PracticeReactApp.Core.Data;
using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Infrastructures.Repositories.Interfaces;

namespace PracticeReactApp.Infrastructures.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        public List<Menu> GetAll()
        {
            return context.Menus.ToList();
        }

        private readonly PracticeDotnetReactContext context;

        public MenuRepository(PracticeDotnetReactContext context)
        {
            this.context = context;
        }
    }
}