using System;
using System.Collections.Generic;

namespace PlacementManagementCell.Models
{
    public partial class Branch
    {
        public Branch()
        {
            Companies = new HashSet<Company>();
            Students = new HashSet<Student>();
        }

        public int BranchId { get; set; }
        public string BranchName { get; set; } = null!;

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
