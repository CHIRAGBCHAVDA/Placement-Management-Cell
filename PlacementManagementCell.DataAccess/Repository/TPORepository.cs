using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using PlacementManagementCell.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                if (companyParams.CompanyId != 0)
                {
                    var oldCompany = _db.Companies.FirstOrDefault(company => company.CompanyId==companyParams.CompanyId);
                    oldCompany.CompanyLogo = companyParams.avatar!=null?companyParams.avatar:oldCompany.CompanyLogo;
                    oldCompany.Name = companyParams.name;
                    oldCompany.Technology = companyParams.technology;
                    oldCompany.Title = companyParams.title;
                    oldCompany.Package = companyParams.package;
                    oldCompany.BriefDesc = companyParams.briefdesc;
                    oldCompany.LongDesc = companyParams.longdesc;
                    oldCompany.FromDate = companyParams.fromdate;
                    oldCompany.ToDate = companyParams.todate;
                    oldCompany.NoOfVacancy = companyParams.vacancy;
                    oldCompany.Deadline = companyParams.deadline;
                    oldCompany.CompanyAddress = companyParams.address;
                    oldCompany.TrainingInfo = companyParams.traininginfo;
                    oldCompany.BenefitsAndPerks = companyParams.benefits;
                    oldCompany.FilePath = companyParams.driveLink;
                    oldCompany.City = companyParams.city;
                    oldCompany.BranchId = companyParams.BID;
                    oldCompany.MinBacklog = companyParams.maxBacklog;
                    oldCompany.MinCgpa = companyParams.minCgpa;

                    _db.Companies.Update(oldCompany);

                    var notification = _db.Notifications.FirstOrDefault(notification => notification.CompanyId == companyParams.CompanyId);
                    notification.CreatedAt = companyParams.deadline;
                    _db.Notifications.Update(notification);

                    _db.SaveChanges();
                }
                else
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
                    
                    var notification = new Notification()
                    {
                        CompanyId = company.CompanyId,
                        CreatedAt = company.Deadline,
                    };
                    _db.Notifications.Add(notification);
                    _db.SaveChanges();
                }
                
                var listOfMails = new List<string>();
                
                baseResponse.StatusCode = 200;
                baseResponse.Message =  "Changes has been saved";
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

        public NewCompanyParams EditCompany(long companyId)
        {
            try
            {
                var company = _db.Companies.FirstOrDefault(company => company.CompanyId == companyId);
                var toAppend = new NewCompanyParams()
                {
                    CompanyId = company.CompanyId,
                    avatar = company.CompanyLogo,
                    name = company.Name,
                    technology = company.Technology,
                    title = company.Title,
                    BID = (int)company.BranchId,
                    package = (long)company.Package,
                    briefdesc = company.BriefDesc,
                    longdesc = company.LongDesc,
                    maxBacklog = (int)company.MinBacklog,
                    minCgpa = (long)company.MinCgpa,
                    fromdate = (DateTime)company.FromDate,
                    todate = (DateTime)company.ToDate,
                    vacancy = (long)company.NoOfVacancy,
                    deadline = (DateTime)company.Deadline,
                    address = company.CompanyAddress,
                    traininginfo = company.TrainingInfo,
                    benefits = company.BenefitsAndPerks,
                    driveLink = company.FilePath,
                    city = company.City,
                };

                return toAppend;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
