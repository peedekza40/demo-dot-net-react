using Microsoft.AspNetCore.Identity;

namespace PracticeReactApp.Server.Models.Entities;

public partial class Role : IdentityRole<string>
{
    public virtual ICollection<RoleApiendpoint> RoleApiendpoints { get; set; } = new List<RoleApiendpoint>();

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
