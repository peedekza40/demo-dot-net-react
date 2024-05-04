using System;
using System.Collections.Generic;

namespace PracticeReactApp.Core.Data.Entities;

public partial class RoleMenu
{
    public long Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string MenuCode { get; set; } = null!;

    public virtual Menu MenuCodeNavigation { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
