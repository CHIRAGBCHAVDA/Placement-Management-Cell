using PlacementManagementCell.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Tokenizer.Symbols;

namespace PlacementManagementCell.DataAccess.Repository.IRepository
{
    public interface ITPORepository
    {
        BaseResponseViewModel AddNewCompany(NewCompanyParams companyParams);
        List<StudentXLSXViewModel> GetStudentApplicationsFromCompanyId(long companyId);
        public NewCompanyParams EditCompany(long companyId);
    }
}
