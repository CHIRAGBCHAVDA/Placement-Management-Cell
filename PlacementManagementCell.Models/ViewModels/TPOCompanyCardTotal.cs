using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class TPOCompanyCardTotal
    {
        public List<TPOCompanyCard> TpoCompanyCards { get; set; } = new List<TPOCompanyCard>();
        public long TotalCompanies { get; set; }

    }
}
