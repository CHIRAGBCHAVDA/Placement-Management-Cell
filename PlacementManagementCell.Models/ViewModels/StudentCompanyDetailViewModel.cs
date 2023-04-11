using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.Models.ViewModels
{
    public class StudentCompanyDetailViewModel
    {
        public StudentHeaderViewModel StudentHeader { get; set; } = new StudentHeaderViewModel();
        public Company Company { get; set; } = new Company();
        public bool IsApplied { get; set; }
    }
}
