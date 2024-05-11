using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NMMODotNetCore.ConsoleApp.Models;

public partial class DotNetTrainingBath4Context : DbContext
{
    public DotNetTrainingBath4Context()
    {
    }

    public DotNetTrainingBath4Context(DbContextOptions<DotNetTrainingBath4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<TableBlog> TableBlogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DELL\\SQLEXPRESS;Database=DotNetTrainingBath4;User ID=sa;Password=sa@123;TrustServerCertificate=true;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Table_Blog");

            entity.Property(e => e.BlogAuthor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BlogContent)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BlogTitle)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsFixedLength();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
