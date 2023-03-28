using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.Models;
using Microsoft.EntityFrameworkCore.Metadata;


namespace PlacementManagementCell.DataAccess.Data
{

    public partial class PlacementManagementContext : DbContext
    {
        public PlacementManagementContext()
        {
        }

        public PlacementManagementContext(DbContextOptions<PlacementManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;; initial catalog=PlacementManagement; Trusted_Connection=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.BenefitsAndPerks).HasColumnType("text");

                entity.Property(e => e.BriefDesc).HasColumnType("text");

                entity.Property(e => e.CompanyAddress).HasColumnType("text");

                entity.Property(e => e.CompanyLogo).HasColumnType("text");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.FilePath).HasColumnType("text");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.LongDesc).HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Technology)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ToDate).HasColumnType("date");

                entity.Property(e => e.TrainingInfo).HasColumnType("text");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.EnrollmentNumber)
                    .HasName("PK__Students__158C2B7062F057CD");

                entity.Property(e => e.EnrollmentNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("enrollment_number");

                entity.Property(e => e.BeCgpa)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("be_cgpa");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.DiplomaCgpa)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("diploma_cgpa");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email_address");

                entity.Property(e => e.EngineeringBranch)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("engineering_branch");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middle_name");

                entity.Property(e => e.MobileNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Resume)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("resume");

                entity.Property(e => e.TenthPercentage)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("tenth_percentage");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("token");

                entity.Property(e => e.TokenCreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("token_created_at");

                entity.Property(e => e.TwelthPercentage)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("twelth_percentage");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }


}
