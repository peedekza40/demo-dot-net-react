using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Core.Data;

public partial class AuthorizeDBContext : IdentityDbContext<User, Role, string>
{
    public AuthorizeDBContext()
    {
    }

    public AuthorizeDBContext(DbContextOptions<PracticeDotnetReactContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var adminUser = new User
        {
            Id = Constants.Base.SystemAdminID,
            UserName = "sysadmin@gmail.com",
            NormalizedUserName = "SYSADMIN@GMAIL.COM",
            PasswordHash = "AQAAAAIAAYagAAAAEPv9/zAx1cchleh9OoCJ626OM8Z+nzSSr13yUa/lru/kAdACgtIPSD4o74IwwS2jKQ==",//Password : pass@word1
            FirstName = "Admin",
            LastName = ".A",
            Email = "sysadmin@gmail.com",
            NormalizedEmail = "SYSADMIN@GMAIL.COM",
            EmailConfirmed = false
        };

        var adminRole = new Role
        {
            Id = Constants.RoleCode.Admin,
            Name = "Admin",
            NormalizedName = "ADMIN"
        };

        var userAdminRole = new IdentityUserRole<string>
        {
            UserId = Constants.Base.SystemAdminID,
            RoleId = Constants.RoleCode.Admin
        };

        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("User").HasData(adminUser);

        builder.Entity<Role>().ToTable("Role").HasData(adminRole);

        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");

        builder.Entity<IdentityUserRole<string>>().ToTable("UserRole").HasData(userAdminRole);

        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim");

        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim");

        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");

        // Apply Snake Case Names for Properties:
        // ApplySnakeCaseNames(builder);
    }

    private void ApplySnakeCaseNames(ModelBuilder builder)
    {
        var mapper = new NpgsqlSnakeCaseNameTranslator();

        foreach (var entity in builder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
            {
                var npgsqlColumnName = mapper.TranslateMemberName(property.GetColumnName());

                property.SetColumnName(npgsqlColumnName);
            }
        }
    }
}