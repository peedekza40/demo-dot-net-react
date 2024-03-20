﻿using Microsoft.AspNetCore.Identity;
using PracticeReactApp.Server.Constants;

namespace PracticeReactApp.Server.ViewModels.Account
{
    public class MaintenanceRoleViewModel: IdentityRole
    {
        public ActionMode Mode { get; set; }
    }
}
