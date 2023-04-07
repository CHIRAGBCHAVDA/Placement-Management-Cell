using Microsoft.AspNetCore.Mvc.Filters;
using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using PlacementManagementCell.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PlacementManagementCell.DataAccess.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PlacementManagementContext _db;
        public static int allCompCount;

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
                                  CompanyLogo = c.CompanyLogo,
                                  City = c.City
                              };
            allCompCount = companyCard.Count();

            return companyCard.ToList();
        }
        public List<CompanyCard> getCompanyApplicationCards(string enrollmentNo)
        {
            var companies = _db.CompanyApplications
            .Where(ca => ca.EnrollmentNo == enrollmentNo)
            .Select(ca => ca.Company)
            .ToList();

            var companyApplicationCards = from c in companies
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
                                              CompanyLogo = c.CompanyLogo,
                                              City = c.City
                                          };
            allCompCount = companyApplicationCards.Count();

            return companyApplicationCards.ToList();

        }

        public CompanyCardsTotalViewModel getCompanyCards(string? searchKeyword, int pageNum, [DefaultValue(1)] int sortBy)
        {
            var filteredCompanies = getCompanyCards();
            allCompCount = filteredCompanies.Count();
            if(searchKeyword != null)
            {
                filteredCompanies = filteredCompanies.Where(cm => cm.Title.ToLower().Contains(searchKeyword.ToLower()) || cm.Technology.Contains(searchKeyword)).ToList();
            }

            switch (sortBy)
            {
                case 1:
                    filteredCompanies = filteredCompanies.OrderBy(c => c.Name).ToList();
                    break;
                case 2:
                    filteredCompanies = filteredCompanies.OrderByDescending(c => c.Package).ToList();
                    break;
                case 3:
                    filteredCompanies = filteredCompanies.OrderBy(c => c.Package).ToList();
                    break;
                case 4:
                    filteredCompanies = filteredCompanies.OrderBy(c => c.Deadline).ToList();
                    break;
            }

            var totalCompanies = filteredCompanies.Count();
                      

            filteredCompanies = filteredCompanies.Skip((pageNum - 1) * 3).Take(3).ToList();

            var CompanyCardTotal = new CompanyCardsTotalViewModel()
            {
                CompanyCards = filteredCompanies,
                TotalCompanies = totalCompanies
            };

            return CompanyCardTotal;
        }
        

        public CompanyCardsTotalViewModel getCompanyApplicationCards(string enrollmentNo,string? searchKeyword, int pageNum, [DefaultValue(1)] int sortBy)
        {
            var filteredApplications = getCompanyApplicationCards(enrollmentNo);
            allCompCount = filteredApplications.Count();
            if (searchKeyword != null)
            {
                filteredApplications = filteredApplications.Where(cm => cm.Title.ToLower().Contains(searchKeyword.ToLower()) || cm.Technology.Contains(searchKeyword)).ToList();
            }

            switch (sortBy)
            {
                case 1:
                    filteredApplications = filteredApplications.OrderBy(c => c.Name).ToList();
                    break;
                case 2:
                    filteredApplications = filteredApplications.OrderByDescending(c => c.Package).ToList();
                    break;
                case 3:
                    filteredApplications = filteredApplications.OrderBy(c => c.Package).ToList();
                    break;
                case 4:
                    filteredApplications = filteredApplications.OrderBy(c => c.Deadline).ToList();
                    break;
            }

            var totalCompanies = filteredApplications.Count();


            filteredApplications = filteredApplications.Skip((pageNum - 1) * 3).Take(3).ToList();

            var CompanyCardTotal = new CompanyCardsTotalViewModel()
            {
                CompanyCards = filteredApplications,
                TotalCompanies = totalCompanies
            };

            return CompanyCardTotal;
        }
        public Company getCompanyById(long companyId)
        {
            var company = _db.Companies.Where(c => c.CompanyId == companyId).FirstOrDefault();
            return company;
        }


        public bool ApplyCompanyById(long companyId,string enrollmentNo)
        {
            try
            {
                var companyApplication = new CompanyApplication()
                {
                    CompanyId = companyId,
                    EnrollmentNo = enrollmentNo
                };
                _db.CompanyApplications.Add(companyApplication);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    
    
    }
}
