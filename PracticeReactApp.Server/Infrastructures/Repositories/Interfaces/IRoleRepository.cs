using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Infrastructures.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        List<Role> GetUserRoles(User user);
    }
}