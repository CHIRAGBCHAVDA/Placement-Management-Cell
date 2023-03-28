using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PlacementManagementContext _db;

        public CompanyRepository(PlacementManagementContext db)
        {
            _db = db;
        }

        public CompanyCard getCompanyCard()
        {
            throw new NotImplementedException();
        }
    }
}
