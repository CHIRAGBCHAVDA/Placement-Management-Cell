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
        public List<CompanyCard> getCompanyCards(int? branchId);
        public CompanyCardsTotalViewModel getCompanyCards(string? searchKeyword,int pageNum, [DefaultValue(1)] int sortBy, int? branchId);
        public Company getCompanyById(long companyId);
        public BaseResponseViewModel ApplyCompanyById(long companyId,string enrollmentNo);
        public CompanyCardsTotalViewModel getCompanyApplicationCards(string enrollmentNo, string? searchKeyword, int pageNum, [DefaultValue(1)] int sortBy);

        public bool IsStudentAppliedForThisCompany(string erNo, long companyId);
        public TPOCompanyCardTotal getTPOCompaniesCard();
    }
}
