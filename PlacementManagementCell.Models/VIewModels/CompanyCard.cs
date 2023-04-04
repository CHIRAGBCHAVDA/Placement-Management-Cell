using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class CompanyCard
    {
        public long CompanyId { get; set; }
        public string? Name { get; set; }
        public string? Technology { get; set; }
        public long? Package { get; set; }
        public string? Title { get; set; }
        public string? BriefDesc { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? NoOfVacancy { get; set; }
        public DateTime? Deadline { get; set; }
        public string? CompanyLogo { get; set; }
        public string? City { get; set; }

    }
}
