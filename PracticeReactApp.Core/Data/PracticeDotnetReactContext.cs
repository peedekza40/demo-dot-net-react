using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Core.Constants;
using PracticeReactApp.Core.Data.Entities;

namespace PracticeReactApp.Core.Data;

public partial class PracticeDotnetReactContext : AuthorizeDBContext
{
    public PracticeDotnetReactContext()
    {
    }

    public PracticeDotnetReactContext(DbContextOptions<PracticeDotnetReactContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apiendpoint> Apiendpoints { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<RoleApiendpoint> RoleApiendpoints { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region seed data
        
        var apiEndpoints = new Apiendpoint[] 
        {
            new Apiendpoint ()
            {
                Code = ApiEndpointCode.Account,
                Name = "Account",
                Path = "Account",
                IsActive = true
            },
            new Apiendpoint ()
            {
                Code = ApiEndpointCode.RoleManagement,
                Name = "Role mangement",
                Path = "RoleManagement",
                IsActive = true
            },
            new Apiendpoint ()
            {
                Code = ApiEndpointCode.TestAware,
                Name = "Test Aware",
                Path = "TestAware",
                IsActive = true
            }
        };

        var roleApiEndpoints = new RoleApiendpoint[]
        {
            new RoleApiendpoint ()
            {
                Id = 1,
                RoleId = RoleCode.Admin,
                Apicode = ApiEndpointCode.Account
            },
            new RoleApiendpoint ()
            {
                Id = 2,
                RoleId = RoleCode.Admin,
                Apicode = ApiEndpointCode.RoleManagement
            },
            new RoleApiendpoint ()
            {
                Id = 3,
                RoleId = RoleCode.Admin,
                Apicode = ApiEndpointCode.TestAware
            },
        };

        var menus = new Menu[]
        {
            new Menu()
            {
                Code = MenuCode.User,
                Name = "User",
                Path = "/user",
                Icon = "ic_user",
                Order = 1,
                IsDisplay = true,
                IsActive = true
            },
            new Menu()
            {
                Code = MenuCode.RoleManagement,
                Name = "Role",
                Path = "/role",
                Icon = "ic_user",
                Order = 2,
                IsDisplay = true,
                IsActive = true
            }
        };

        var roleMenus = new RoleMenu[]
        { 
            new RoleMenu()
            {
                Id = 1,
                RoleId = RoleCode.Admin,
                MenuCode = MenuCode.User
            },
            new RoleMenu()
            { 
                Id = 2,
                RoleId = RoleCode.Admin,
                MenuCode = MenuCode.RoleManagement,
            }
        };
        
        #endregion

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Apiendpoint>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("APIEndpoint");

            entity.HasIndex(e => e.Code, "APIEndpointIndex").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.Path).HasMaxLength(256);

            entity.HasData(apiEndpoints);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Menu");

            entity.HasIndex(e => e.Code, "MenuIndex").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Attribute).HasMaxLength(1000);
            entity.Property(e => e.Icon).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.ParentCode).HasMaxLength(50);
            entity.Property(e => e.Path).HasMaxLength(256);

            entity.HasData(menus);
        });

        modelBuilder.Entity<RoleApiendpoint>(entity =>
        {
            entity.ToTable("RoleAPIEndpoint");

            entity.HasIndex(e => e.Id, "RoleAPIEndpointIndex");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Apicode)
                .HasMaxLength(50)
                .HasColumnName("APICode");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.ApicodeNavigation).WithMany(p => p.RoleApiendpoints).HasForeignKey(d => d.Apicode);

            entity.HasOne(d => d.Role).WithMany(p => p.RoleApiendpoints)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleAPIEndpoint_Role_RoleId");

            entity.HasData(roleApiEndpoints);
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.ToTable("RoleMenu");

            entity.HasIndex(e => e.Id, "RoleMenuIndex");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MenuCode).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");

            entity.HasOne(d => d.MenuCodeNavigation).WithMany(p => p.RoleMenus).HasForeignKey(d => d.MenuCode);

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_RoleMenu_Role_RoleId");

            entity.HasData(roleMenus);
        });
    }
}