using System;
using System.Collections.Generic;

namespace PracticeReactApp.Server.Models.Entities;

public partial class Apiendpoint
{
    public string Code { get; set; } = null!;

    public string? Name { get; set; }

    public string? Path { get; set; }

    public bool? IsActive { get; set; }

    public virtual ICollection<RoleApiendpoint> RoleApiendpoints { get; set; } = new List<RoleApiendpoint>();
}
