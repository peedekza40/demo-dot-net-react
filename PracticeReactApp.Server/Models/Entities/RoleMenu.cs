using System;
using System.Collections.Generic;

namespace PracticeReactApp.Server.Models.Entities;

public partial class RoleMenu
{
    public long Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string MenuCode { get; set; } = null!;
}
