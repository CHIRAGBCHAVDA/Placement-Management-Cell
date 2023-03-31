using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class CompanyCardsTotalViewModel
    {
        public List<CompanyCard> CompanyCards { get; set; } = new List<CompanyCard>();
        public long TotalCompanies { get;set; } = 0;
    }
}
