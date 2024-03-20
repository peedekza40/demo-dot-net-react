using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace PracticeReactApp.Server.Models
{
    public partial class User: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
