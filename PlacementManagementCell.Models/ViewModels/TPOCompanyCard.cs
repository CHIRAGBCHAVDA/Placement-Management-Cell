using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class TPOCompanyCard
    {
        public long CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Technology { get; set; } = string.Empty;
        public string? CompanyLogo { get; set; }
        public long StudentsApplied { get; set; }
        public long? Package { get; set; }
        public int? BranchId { get; set; }
        public DateTime? Deadline { get; set; }
    }
}
