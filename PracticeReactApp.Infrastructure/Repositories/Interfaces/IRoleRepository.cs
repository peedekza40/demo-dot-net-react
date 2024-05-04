using PracticeReactApp.Core.Data.Entities;
using PracticeReactApp.Core.Models;

namespace PracticeReactApp.Infrastructures.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        DataTableResponseModel<Role> GetDataTableResponse(DataTableRequestModel dataTableRequest);

        List<Role> GetUserRoles(User user);

        bool IsExists(string id);

        void Insert(Role model);

        void Update(Role model);
    }
}