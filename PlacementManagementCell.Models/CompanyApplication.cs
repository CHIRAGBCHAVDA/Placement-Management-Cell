using System;
using System.Collections.Generic;

namespace PlacementManagementCell.Models
{
    public partial class CompanyApplication
    {
        public long ApplicationId { get; set; }
        public string? EnrollmentNo { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Company? Company { get; set; }
        public virtual Student? EnrollmentNoNavigation { get; set; }
    }
}
