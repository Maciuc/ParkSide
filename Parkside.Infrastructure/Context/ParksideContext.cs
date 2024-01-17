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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Championship> Championships { get; set; } = null!;
        public virtual DbSet<Coach> Coaches { get; set; } = null!;
        public virtual DbSet<Match> Matches { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<Player> Players { get; set; } = null!;
        public virtual DbSet<SocialMedia> SocialMedias { get; set; } = null!;
        public virtual DbSet<Sponsor> Sponsors { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\Local;Initial Catalog=Parkside;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.Property(e => e.RoleId).HasMaxLength(450);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.Property(e => e.CreatedDate).HasColumnType("date");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FidelityPoints).HasDefaultValueSql("((0))");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.ImgUrl).HasMaxLength(300);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Championship>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Coach>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .HasColumnName("First_Name");

                entity.Property(e => e.Height).HasMaxLength(200);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.TeamName).HasMaxLength(200);
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EnemyTeamPoints).HasMaxLength(10);

                entity.Property(e => e.Location).HasMaxLength(500);

                entity.Property(e => e.MainTeamPoints).HasMaxLength(10);

                entity.HasOne(d => d.Championship)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.ChampionshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Matches__Champio__7B264821");

                entity.HasOne(d => d.EnemyTeam)
                    .WithMany(p => p.Matches)
                    .HasForeignKey(d => d.EnemyTeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Matches__EnemyTe__7C1A6C5A");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.IsPublished).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.PublishedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .HasColumnName("First_Name");

                entity.Property(e => e.Height).HasMaxLength(10);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Nationality).HasMaxLength(100);

                entity.Property(e => e.Number).HasMaxLength(10);

                entity.Property(e => e.Role).HasMaxLength(50);

                entity.Property(e => e.TeamName).HasMaxLength(200);
            });

            modelBuilder.Entity<SocialMedia>(entity =>
            {
                entity.Property(e => e.Link).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Platform).HasMaxLength(200);
            });

            modelBuilder.Entity<Sponsor>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(500)
                    .HasColumnName("ImageURL");

                entity.Property(e => e.Name).HasMaxLength(200);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
