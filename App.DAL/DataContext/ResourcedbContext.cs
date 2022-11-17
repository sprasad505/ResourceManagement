using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using App.DAL.Models;

namespace App.DAL.DataContext
{
    public partial class ResourcedbContext : DbContext
    {
        public ResourcedbContext()
        {
        }

        public ResourcedbContext(DbContextOptions<ResourcedbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Allocation> Allocations { get; set; } = null!;
        public virtual DbSet<Project> Projects { get; set; } = null!;
        public virtual DbSet<Resource> Resources { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=shankarp-817\\sqlexpress;Initial Catalog=Resourcedb;Integrated Security=True");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Allocation>(entity =>
            {
                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Allocations)
                    .HasPrincipalKey(p => p.EmployeeId)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("Employeef");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Allocations)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("Projectf");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.Allocations)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("Teamf");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Resource>(entity =>
            {
                entity.HasIndex(e => e.EmployeeId, "AK_Resources_Column")
                    .IsUnique();

                entity.Property(e => e.Designation).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
