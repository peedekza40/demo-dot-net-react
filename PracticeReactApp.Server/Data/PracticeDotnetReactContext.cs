using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PracticeReactApp.Server.Models.Entities;

namespace PracticeReactApp.Server.Data;

public partial class PracticeDotnetReactContext : AuthorizeDBContext
{
    public PracticeDotnetReactContext()
    {
    }

    public PracticeDotnetReactContext(DbContextOptions<PracticeDotnetReactContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Menu");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Attribute).HasMaxLength(1000);
            entity.Property(e => e.Icon).HasMaxLength(256);
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.ParentCode).HasMaxLength(50);
            entity.Property(e => e.Path).HasMaxLength(256);
        });

        builder.Entity<RoleMenu>(entity =>
        {
            entity.ToTable("RoleMenu");

            entity.Property(e => e.MenuCode).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
