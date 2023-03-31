using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class StudentCompanyViewModel
    {

        public StudentHeaderViewModel StudentHeader { get; set; } = new StudentHeaderViewModel();

        public CompanyCardsTotalViewModel CompanyCardsTotal { get; set; } = new CompanyCardsTotalViewModel();
    }
}
