using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepo { get; }
        ICompanyRepository CompanyRepo { get; }
        void Save();
    }
}
