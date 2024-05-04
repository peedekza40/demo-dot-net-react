using System;
using System.Collections.Generic;

namespace PracticeReactApp.Core.Data.Entities;

public partial class Menu
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public string? Path { get; set; }

    public string? Attribute { get; set; }

    public string? Icon { get; set; }

    public string? ParentCode { get; set; }

    public int? Order { get; set; }

    public bool? IsDisplay { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RoleMenu> RoleMenus { get; set; } = new List<RoleMenu>();
}
