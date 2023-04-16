using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KodingBlog.Models;

public partial class KodingBlogContext : DbContext
{

    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public KodingBlogContext()
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public KodingBlogContext(DbContextOptions<KodingBlogContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
