using PlacementManagementCell.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository
    {
        public List<CompanyCard> getCompanyCards();
        public List<CompanyCard> getCompanyCards(string? searchKeyword,int pageNum, [DefaultValue(1)] int sortBy);
    }
}
