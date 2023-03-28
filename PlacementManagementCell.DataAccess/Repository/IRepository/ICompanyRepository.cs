using PlacementManagementCell.Models.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository
    {
        public CompanyCard getCompanyCard();
    }
}
