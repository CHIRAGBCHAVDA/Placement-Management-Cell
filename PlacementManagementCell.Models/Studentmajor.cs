using System;
using System.Collections.Generic;

namespace PlacementManagementCell.Models
{
    public partial class Studentmajor
    {
        public string EnrollmentNo { get; set; } = null!;
        public string? MobileNo { get; set; }
        public string EmailId { get; set; } = null!;
        public string? Token { get; set; }
    }
}
