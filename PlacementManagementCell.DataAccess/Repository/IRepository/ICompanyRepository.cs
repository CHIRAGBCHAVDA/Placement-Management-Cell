using PlacementManagementCell.Models;
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
        public CompanyCardsTotalViewModel getCompanyCards(string? searchKeyword,int pageNum, [DefaultValue(1)] int sortBy);
        public Company getCompanyById(long companyId);
        public bool ApplyCompanyById(long companyId,string enrollmentNo);
    }
}
