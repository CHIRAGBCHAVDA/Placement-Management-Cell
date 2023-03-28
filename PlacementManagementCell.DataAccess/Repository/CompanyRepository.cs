using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PlacementManagementCell.DataAccess.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PlacementManagementContext _db;

        public CompanyRepository(PlacementManagementContext db)
        {
            _db = db;
        }

        public List<CompanyCard> getCompanyCards()
        {
            var companyCard = from c in _db.Companies
                              where c.NoOfVacancy > 0
                              select new CompanyCard()
                              {
                                  CompanyId = c.CompanyId,
                                  Name = c.Name,
                                  Technology = c.Technology,
                                  Package = c.Package,
                                  Title = c.Title,
                                  BriefDesc = c.BriefDesc,
                                  FromDate = c.FromDate,
                                  ToDate = c.ToDate,
                                  NoOfVacancy = c.NoOfVacancy,
                                  Deadline = c.Deadline,
                                  CompanyLogo = c.CompanyLogo
                              };

            return companyCard.ToList();
        }
    }
}
