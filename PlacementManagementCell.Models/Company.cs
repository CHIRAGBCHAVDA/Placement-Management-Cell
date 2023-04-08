using System;
using System.Collections.Generic;

namespace PlacementManagementCell.Models
{
    public partial class Company
    {
        public Company()
        {
            CompanyApplications = new HashSet<CompanyApplication>();
        }

        public long CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Technology { get; set; }
        public long? Package { get; set; }
        public string? Title { get; set; }
        public string? BriefDesc { get; set; }
        public string? LongDesc { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? NoOfVacancy { get; set; }
        public DateTime? Deadline { get; set; }
        public string? CompanyAddress { get; set; }
        public string? TrainingInfo { get; set; }
        public string? BenefitsAndPerks { get; set; }
        public string? FilePath { get; set; }
        public string? CompanyLogo { get; set; }
        public string? City { get; set; }
        public int? BranchId { get; set; }
        public int? MinBacklog { get; set; }
        public decimal? MinCgpa { get; set; }

        public virtual Branch? Branch { get; set; }
        public virtual ICollection<CompanyApplication> CompanyApplications { get; set; }
    }
}
