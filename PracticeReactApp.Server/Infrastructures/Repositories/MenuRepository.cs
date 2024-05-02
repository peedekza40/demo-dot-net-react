using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeReactApp.Server.Data;
using PracticeReactApp.Server.Infrastructures.Repositories.Interfaces;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Infrastructures.Repositories
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