using Microsoft.AspNetCore.Identity;

namespace PracticeReactApp.Core.Data.Entities;

public partial class Role : IdentityRole<string>
{
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public virtual ICollection<RoleApiendpoint> RoleApiendpoints { get; set; } = new List<RoleApiendpoint>();

    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
