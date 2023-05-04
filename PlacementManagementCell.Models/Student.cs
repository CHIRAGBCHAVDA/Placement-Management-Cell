using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlacementManagementCell.Models
{
    public partial class Student
    {
        public Student()
        {
            CompanyApplications = new HashSet<CompanyApplication>();
        }

        public string EnrollmentNumber { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public decimal TenthPercentage { get; set; }
        public decimal TwelthPercentage { get; set; }
        public decimal DiplomaCgpa { get; set; }
        public decimal BeCgpa { get; set; }
        public string MobileNumber { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;

        [NotMapped]
        [Compare("Password",ErrorMessage ="Password and Confirm Password must be matched")]
        public string ConfirmPassword { get; set; }
        public string? Resume { get; set; }
        public string? Token { get; set; }
        public DateTime? TokenCreatedAt { get; set; }
        public string? Avatar { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? BranchId { get; set; }
        public int? ActiveBacklog { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual ICollection<CompanyApplication> CompanyApplications { get; set; }
    }
}
