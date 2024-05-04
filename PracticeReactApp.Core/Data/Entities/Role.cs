using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace PracticeReactApp.Core.Data.Entities;

public partial class Role : IdentityRole<string>
{
    [JsonIgnore]
    public virtual ICollection<RoleApiendpoint> RoleApiendpoints { get; set; } = new List<RoleApiendpoint>();

    [JsonIgnore]
    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
