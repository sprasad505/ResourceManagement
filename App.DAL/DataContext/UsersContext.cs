using App.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.DAL.DataContext
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /* if (!optionsBuilder.IsConfigured)
             {
 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                 optionsBuilder.UseSqlServer("Data Source=shankarp-817\\sqlexpress;Initial Catalog=users;Integrated Security=True");
             }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK__tmp_ms_x__F3DBC573EA2880E9");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.PasswordHash).HasColumnName("passwordHash");

                entity.Property(e => e.PasswordSalt).HasColumnName("passwordSalt");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
