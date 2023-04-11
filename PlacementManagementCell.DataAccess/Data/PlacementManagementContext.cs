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

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Company> Companies { get; set; } = null!;
        public virtual DbSet<CompanyApplication> CompanyApplications { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Studentmajor> Studentmajors { get; set; } = null!;

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
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("branch");

                entity.Property(e => e.BranchId)
                    .ValueGeneratedNever()
                    .HasColumnName("branch_id");

                entity.Property(e => e.BranchName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("branch_name");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.BenefitsAndPerks).HasColumnType("text");

                entity.Property(e => e.BranchId)
                    .HasColumnName("branch_id")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.BriefDesc).HasColumnType("text");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyAddress).HasColumnType("text");

                entity.Property(e => e.CompanyLogo).HasColumnType("text");

                entity.Property(e => e.Deadline).HasColumnType("date");

                entity.Property(e => e.FilePath).HasColumnType("text");

                entity.Property(e => e.FromDate).HasColumnType("date");

                entity.Property(e => e.LongDesc).HasColumnType("text");

                entity.Property(e => e.MinBacklog)
                    .HasColumnName("min_backlog")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.MinCgpa)
                    .HasColumnType("decimal(3, 2)")
                    .HasColumnName("min_cgpa")
                    .HasDefaultValueSql("((2.3))");

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

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Companies)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Company__branch___2A164134");
            });

            modelBuilder.Entity<CompanyApplication>(entity =>
            {
                entity.HasKey(e => e.ApplicationId)
                    .HasName("PK__companyA__3BCBDCF27D2C5E14");

                entity.ToTable("companyApplication");

                entity.Property(e => e.ApplicationId).HasColumnName("application_id");

                entity.Property(e => e.CompanyId).HasColumnName("companyId");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DeletedAt)
                    .HasColumnType("date")
                    .HasColumnName("deleted_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EnrollmentNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("enrollment_no");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.CompanyApplications)
                    .HasForeignKey(d => d.CompanyId)
                    .HasConstraintName("FK__companyAp__compa__03F0984C");

                entity.HasOne(d => d.EnrollmentNoNavigation)
                    .WithMany(p => p.CompanyApplications)
                    .HasForeignKey(d => d.EnrollmentNo)
                    .HasConstraintName("FK__companyAp__enrol__02FC7413");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.EnrollmentNumber)
                    .HasName("PK__Students__158C2B7062F057CD");

                entity.Property(e => e.EnrollmentNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("enrollment_number");

                entity.Property(e => e.ActiveBacklog)
                    .HasColumnName("active_backlog")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Avatar).HasColumnType("text");

                entity.Property(e => e.BeCgpa)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("be_cgpa");

                entity.Property(e => e.BranchId).HasColumnName("branch_id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

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

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.IsVerified)
                    .HasColumnName("is_verified")
                    .HasDefaultValueSql("((0))");

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

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.BranchId)
                    .HasConstraintName("FK__Students__branch__2B0A656D");
            });

            modelBuilder.Entity<Studentmajor>(entity =>
            {
                entity.HasKey(e => e.EnrollmentNo)
                    .HasName("PK__studentm__6D24831997387823");

                entity.ToTable("studentmajor");

                entity.Property(e => e.EnrollmentNo)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("enrollment_no");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email_id");

                entity.Property(e => e.MobileNo)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("mobile_no");

                entity.Property(e => e.Token)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("token");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
