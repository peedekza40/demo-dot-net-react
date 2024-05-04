using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Server.ViewModels.RoleManagement
{
    public class MaintenanceRoleViewModel : Role
    {
        public ActionMode Mode { get; set; }
    }
}