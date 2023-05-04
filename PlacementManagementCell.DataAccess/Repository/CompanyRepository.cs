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

        public List<CompanyCard> getCompanyCards(int? branchId)
        {
                var companyCard = from c in _db.Companies
                              where c.BranchId == branchId || c.BranchId==1
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
                                  City = c.City,
                                  BranchId = c.BranchId
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

        public CompanyCardsTotalViewModel getCompanyCards(string? searchKeyword, int pageNum, [DefaultValue(1)] int sortBy,int? branchId)
        {
            var filteredCompanies = getCompanyCards(branchId);
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


        public BaseResponseViewModel ApplyCompanyById(long companyId,string enrollmentNo)
        {
            BaseResponseViewModel baseResponse = new BaseResponseViewModel();
            try
            {
                var company = getCompanyById(companyId);
                var student = _db.Students.FirstOrDefault(student => student.EnrollmentNumber.Equals(enrollmentNo));

                if(student.BeCgpa >= company.MinCgpa && student.ActiveBacklog<=company.MinBacklog && (student.BranchId==company.BranchId || company.BranchId==1))   
                {
                    var companyApplication = new CompanyApplication()
                    {
                        CompanyId = companyId,
                        EnrollmentNo = enrollmentNo
                    };
                    _db.CompanyApplications.Add(companyApplication);
                    _db.SaveChanges();
                    baseResponse.StatusCode = 200;
                    baseResponse.Success = true;
                    baseResponse.Message = "Your application is submitted!!";
                }
                else
                {
                    baseResponse.StatusCode = 500;
                    baseResponse.Success = false;
                    baseResponse.Message = "You're Not meeting company's eligibility criteria....!!";
                }
                
                
                return baseResponse;
            }
            catch(Exception ex)
            {
                return baseResponse;
            }
        }

        public bool IsStudentAppliedForThisCompany(string erNo, long companyId)
        {
           var isApplied =  _db.CompanyApplications.FirstOrDefault(company => company.EnrollmentNo.Equals(erNo) && company.CompanyId == companyId);
            if (isApplied != null) return true;
            return false;
        }

        public TPOCompanyCardTotal getTPOCompaniesCard()
        {
            var comp = _db.Companies.Select(company => new TPOCompanyCard()
            {
                CompanyId = company.CompanyId,
                CompanyName = company.Name,
                Technology = company.Technology,
                CompanyLogo = company.CompanyLogo,
                BranchId = company.BranchId,
                Deadline = company.Deadline,
                Package = company.Package,
                StudentsApplied = company.CompanyApplications.Count()
            });

            TPOCompanyCardTotal toReturn = new TPOCompanyCardTotal()
            {
                TpoCompanyCards = comp.ToList(),
                TotalCompanies = comp.Count()
            };
            return toReturn;
        }

        public TPOCompanyCardTotal getTPOCompaniesCard(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            var compCards = getTPOCompaniesCard().TpoCompanyCards;
            if (searchKeyword != null)
            {
                compCards = compCards.Where(c => c.CompanyName.ToLower().Contains(searchKeyword.ToLower())).ToList();
            }

            switch (sortBy)
            {
                case 1:
                    compCards = compCards.OrderBy(c => c.CompanyName).ToList();
                    break;
                case 2:
                    compCards = compCards.OrderByDescending(c => c.Package).ToList();
                    break;
                case 3:
                    compCards = compCards.OrderBy(c => c.Package).ToList();
                    break;
                case 4:
                    compCards = compCards.OrderBy(c => c.Deadline).ToList();
                    break;
            }

            var compcardsTotal = compCards.Count();
            compCards = compCards.Skip((pageNum - 1) * 3).Take(3).ToList();

            TPOCompanyCardTotal cardsToReturn = new TPOCompanyCardTotal()
            {
                TotalCompanies = compcardsTotal,
                TpoCompanyCards = compCards
            };

            return cardsToReturn;
        }

        public ChartDataViewModel GetChartData(long companyId)
        {
            var temp = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId== 1).Count();

            var temp2 = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.Company.BranchId == 3).Count();


            var IT = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId == 2).Count();

            var Computer = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId == 3).Count();
    
            var EC = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId == 4).Count();


            var Mech = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId == 5).Count();



            var Civil = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId== 6).Count();


            var Prod = _db.CompanyApplications.Where(c => c.CompanyId == companyId && c.EnrollmentNoNavigation.BranchId == 7).Count();

            ChartDataViewModel toReturn = new ChartDataViewModel()
            {
                IT = IT,
                Computer = Computer,
                EC = EC,
                Mech = Mech,
                Civil = Civil ,
                Prod = Prod 
            };

            return toReturn;
        }

    }
}
