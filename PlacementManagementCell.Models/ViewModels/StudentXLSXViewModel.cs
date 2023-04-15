using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class StudentXLSXViewModel
    {
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
        public string? Resume { get; set; }
        public string? Avatar { get; set; }
        public int? ActiveBacklog { get; set; }
        public string BranchName { get; set; } = null!;

    }
}
