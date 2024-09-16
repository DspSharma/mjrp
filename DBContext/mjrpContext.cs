using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MJRPAdmin.Entities;

namespace MJRPAdmin.DBContext
{
    public partial class mjrpContext : DbContext
    {
        public mjrpContext()
        {
        }

        public mjrpContext(DbContextOptions<mjrpContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AcademicProg> AcademicProgs { get; set; } = null!;
        public virtual DbSet<UploadResult> UploadResults { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("name=dbCon", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.6.12-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_general_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<AcademicProg>(entity =>
            {
                entity.HasKey(e => e.AcdProId)
                    .HasName("PRIMARY");

                entity.ToTable("AcademicProg");

                entity.Property(e => e.AcdProId)
                    .HasColumnType("int(11)")
                    .HasColumnName("AcdProID");

                entity.Property(e => e.AcdProDesc).HasMaxLength(300);

                entity.Property(e => e.AcdProIsDeleted)
                    .HasColumnType("bit(1)")
                    .HasColumnName("AcdProIs_Deleted");

                entity.Property(e => e.AcdProNm)
                    .HasMaxLength(100)
                    .HasColumnName("AcdProNM");

                entity.Property(e => e.DisplayPriority).HasColumnType("int(11)");

                entity.Property(e => e.HeaderImg).HasMaxLength(100);
            });

            modelBuilder.Entity<UploadResult>(entity =>
            {
                entity.HasKey(e => e.RecId)
                    .HasName("PRIMARY");

                entity.ToTable("UploadResult");

                entity.Property(e => e.RecId).HasColumnType("int(11)");

                entity.Property(e => e.CollegeId).HasColumnType("int(11)");

                entity.Property(e => e.DisplayPriority).HasColumnType("int(11)");

                entity.Property(e => e.FacultyId).HasColumnType("int(11)");

                entity.Property(e => e.FileName).HasMaxLength(100);

                entity.Property(e => e.ModifyDate).HasColumnType("datetime");

                entity.Property(e => e.NoOfRowsToDisplay).HasColumnType("int(11)");

                entity.Property(e => e.ResultDate).HasColumnType("datetime");

                entity.Property(e => e.ResultDescription).HasMaxLength(500);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.RecId)
                    .HasName("PRIMARY");

                entity.Property(e => e.RecId).HasColumnType("int(11)");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.IsActivated).HasColumnType("bit(1)");

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password).HasMaxLength(128);

                entity.Property(e => e.UserId).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
