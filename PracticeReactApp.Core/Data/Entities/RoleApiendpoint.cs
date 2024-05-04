using System;
using System.Collections.Generic;

namespace PracticeReactApp.Core.Data.Entities;

public partial class RoleApiendpoint
{
    public long Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string Apicode { get; set; } = null!;

    public virtual Apiendpoint ApicodeNavigation { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
