using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Parkside.Infrastructure.Entities;

namespace Parkside.Infrastructure.Context
{
    public partial class ParksideContext : DbContext
    {
        public ParksideContext()
        {
        }

        public ParksideContext(DbContextOptions<ParksideContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=tcp:parkisde.database.windows.net,1433;Initial Catalog=ParksideDatabase;Persist Security Info=False;Trusted_Connection=False;Encrypt=True;TrustServerCertificate=False;Authentication=Active Directory Default;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("First_Name");

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(200)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("Last_Name");
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(200)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
