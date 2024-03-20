using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Server.Models;

namespace PracticeReactApp.Server.Data;

public partial class PracticeReactContext : IdentityDbContext<User>
{
    public PracticeReactContext()
    {
    }

    public PracticeReactContext(DbContextOptions<PracticeReactContext> options)
        : base(options)
    {
    }
}