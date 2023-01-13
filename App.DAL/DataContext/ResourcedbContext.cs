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
        public virtual DbSet<Calendar22> Calender22s { get; set; } = null!;
        public virtual DbSet<Sprint> Sprints { get; set; } = null!;
        public virtual DbSet<Point> Points { get; set; } = null!;
        public virtual DbSet<Story> Stories { get; set; } = null!;
        public virtual DbSet<Leave> Leaves { get; set; } = null!;

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
                entity.HasIndex(e => e.EmployeeId, "empid")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Resources)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Resources_Project");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("Team");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Teams)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Team_Project");
            });

            modelBuilder.Entity<Sprint>(entity =>
            {
                entity.ToTable("Sprint");

                entity.Property(e => e.endDate).HasColumnType("datetime");

                entity.Property(e => e.name).HasMaxLength(50);

                entity.Property(e => e.startDate).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Sprints)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Sprint_Project");
            });

            modelBuilder.Entity<Calendar22>(entity =>
            {
                entity.HasKey(e => e.Date)
                    .HasName("PK__Calender__77387D067ED05455");

                entity.ToTable("Calender22");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Point>(entity =>
            {
                entity.ToTable("Point");

                entity.HasOne(d => d.Story)
                    .WithMany(p => p.Points)
                    .HasForeignKey(d => d.StoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Storykey");
            });


            modelBuilder.Entity<Story>(entity =>
            {
                entity.ToTable("Story");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.ModifiedOn).HasColumnType("datetime");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Story_Project");

                entity.HasOne(d => d.Sprint)
                    .WithMany(p => p.Stories)
                    .HasForeignKey(d => d.SprintId)
                    .HasConstraintName("FK_Story_Sprint");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.ToTable("Leave");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveDate).HasColumnType("date");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Leaves)
                    .HasPrincipalKey(p => p.EmployeeId)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Leave_Resource");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
