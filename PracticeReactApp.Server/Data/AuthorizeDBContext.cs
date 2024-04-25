using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Data;

public partial class AuthorizeDBContext : IdentityDbContext<User>
{
    public AuthorizeDBContext()
    {
    }

    public AuthorizeDBContext(DbContextOptions<PracticeDotnetReactContext> options)
        : base(options)
    {
    }

    protected  override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("User");
        builder.Entity<IdentityRole>().ToTable("Role");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
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