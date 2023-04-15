using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using PlacementManagementCell.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository
{
    public class TPORepository:ITPORepository
    {
        private readonly PlacementManagementContext _db;
        public TPORepository(PlacementManagementContext db)
        {
            _db = db;
        }

        public BaseResponseViewModel AddNewCompany(NewCompanyParams companyParams)
        {
           var baseResponse = new BaseResponseViewModel();
            try
            {
                Company company = new Company()
                {
                    CompanyLogo = companyParams.avatar,
                    Name = companyParams.name,
                    Technology = companyParams.technology,
                    Title = companyParams.title,
                    Package = companyParams.package,
                    BriefDesc = companyParams.briefdesc,
                    LongDesc = companyParams.longdesc,
                    FromDate = companyParams.fromdate,
                    ToDate = companyParams.todate,
                    NoOfVacancy = companyParams.vacancy,
                    Deadline = companyParams.deadline,
                    CompanyAddress = companyParams.address,
                    TrainingInfo = companyParams.traininginfo,
                    BenefitsAndPerks = companyParams.benefits,
                    FilePath = companyParams.driveLink,
                    City = companyParams.city,
                    BranchId = companyParams.BID,
                    MinBacklog = companyParams.maxBacklog,
                    MinCgpa = companyParams.minCgpa
                };

                _db.Companies.Add(company);
                _db.SaveChanges();

                baseResponse.StatusCode = 200;
                baseResponse.Message =  company.Name + " added successfully";
                baseResponse.Success = true;

            }
            catch(Exception ex)
            {
                baseResponse.StatusCode = 500;
                baseResponse.Message = "Some erroe occurred while saving Info....!";
                baseResponse.Success = false;
            }
            return baseResponse;
        }

        public List<StudentXLSXViewModel> GetStudentApplicationsFromCompanyId(long companyId)
        {
            var temp = _db.CompanyApplications.Where(ca => ca.CompanyId == companyId).AsQueryable()
               .Select(student => new StudentXLSXViewModel()
               {
                   EnrollmentNumber = student.EnrollmentNoNavigation.EnrollmentNumber,
                   FirstName = student.EnrollmentNoNavigation.FirstName,
                   MiddleName = student.EnrollmentNoNavigation.MiddleName,
                   LastName = student.EnrollmentNoNavigation.LastName,
                   DateOfBirth = student.EnrollmentNoNavigation.DateOfBirth,
                   TenthPercentage = student.EnrollmentNoNavigation.TenthPercentage,
                   TwelthPercentage = student.EnrollmentNoNavigation.TwelthPercentage,
                   DiplomaCgpa = student.EnrollmentNoNavigation.DiplomaCgpa,
                   BeCgpa = student.EnrollmentNoNavigation.BeCgpa,
                   MobileNumber = student.EnrollmentNoNavigation.MobileNumber,
                   EmailAddress = student.EnrollmentNoNavigation.EmailAddress,
                   Resume = student.EnrollmentNoNavigation.Resume,
                   Avatar = student.EnrollmentNoNavigation.Avatar,
                   BranchName = student.EnrollmentNoNavigation.Branch.BranchName,
                   ActiveBacklog = student.EnrollmentNoNavigation.ActiveBacklog
               }).ToList();

            return temp;
        }
    }
}
