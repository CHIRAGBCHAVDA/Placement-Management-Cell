using CsvHelper;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using Placement_Management_Cell.Services;
using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using PlacementManagementCell.Models.ViewModels;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Xml.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.Mime.MediaTypeNames;

namespace Placement_Management_Cell.Controllers
{
    public class TPOController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PlacementManagementContext _db;

        public TPOController(ILogger<StudentController> logger, IUnitOfWork unitOfWork, PlacementManagementContext db)
        {
            _logger = logger;
            _db = db;
            _unitOfWork = unitOfWork;
        }
        
        public IActionResult TPOLanding([DefaultValue(0)]long companyId)
        {
            if (companyId == 0)
            {
                return View(new NewCompanyParams());
            }
            else
            {
                NewCompanyParams toReturn = _unitOfWork.TPORepo.EditCompany((long)companyId);
                return View(toReturn);
            }
        }

       
        [HttpPost]
        public IActionResult NewCompany(NewCompanyParams companyParams)
            {
            var viewModel = _unitOfWork.TPORepo.AddNewCompany(companyParams);
            var company = _db.Companies.First(c => c.Name.Equals(companyParams.name));
            var listOfMails = _db.Students.Where(s => (s.BranchId == company.BranchId || companyParams.BID == 1)).Select(s => s.EmailAddress).ToList();

            var strMails = string.Join(',', listOfMails);

                string emailBody = @"<div style=""background-color: #f5f5f5; padding: 20px;"">
                              <div style=""background-color: #ffffff; max-width: 600px; margin: 0 auto; border-radius: 10px; overflow: hidden; box-shadow: 0px 0px 20px rgba(0, 0, 0, 0.1);"">
                                <div style=""background-color: #f5f5f5; padding: 20px; text-align: center;"">
                                  <img src=""" + companyParams.avatar + @""" alt=""User Avatar"" style=""width: 100px; height: 100px; border-radius: 50%; object-fit: cover; border: 5px solid #ffffff;"">
                                  <h1 style=""font-size: 24px; margin: 10px 0 0; color: #333333;"">New job posting</h1>
                                  <h2 style=""font-size: 18px; margin: 5px 0; color: #666666;"">Hi, Student" + @"</h2>
                                </div>
                                <div style=""padding: 20px;"">
                                  <h3 style=""font-size: 20px; margin: 0 0 20px; color: #333333;"">" + companyParams.title + @"</h3>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Technology: " + companyParams.technology + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Package: " + companyParams.package + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">" + companyParams.briefdesc + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">" + companyParams.longdesc + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Max Backlog: " + companyParams.maxBacklog + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Min CGPA: " + companyParams.minCgpa + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Vacancy: " + companyParams.vacancy + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Deadline: " + companyParams.deadline.ToString().Substring(0,11) + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Benefits: " + companyParams.benefits + @"</p>
                                  <p style=""font-size: 16px; margin: 0 0 20px; color: #666666;"">Address: " + companyParams.address + @"</p>
                                </div>
                              </div>
                            </div>";

            string subject = "Greetings from Placement Cell : " + companyParams.title;

            EmailSender.SendEmail(strMails, emailBody, subject);
            
            return Json(viewModel);
        }

        public IActionResult TPOAllCompanyDashboard(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            TPOCompanyCardTotal viewModel = _unitOfWork.CompanyRepo.getTPOCompaniesCard(searchKeyword, pageNum, sortBy);
            return View(viewModel);
        }

        public IActionResult TPOCompCardFilter(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            var Comp = new TPOCompanyCardTotal();
            Comp = _unitOfWork.CompanyRepo.getTPOCompaniesCard(searchKeyword, pageNum, sortBy);
            return PartialView("_TPOCompCardDashboard",Comp);
        }
        //[HttpPost]
        public IActionResult DownloadStudentDetails(long companyId)
        {

            var model = _unitOfWork.TPORepo.GetStudentApplicationsFromCompanyId(companyId);
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 1024, true))
            {
                var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(model);
            }

            stream.Position = 0;
            var contentType = "text/csv";
            var fileName = "students.csv";
            TempData["jovamate"] = "File Downloaded";
            return File(stream, contentType, fileName);

        }

        public IActionResult getChartData(long companyId)
        {
            var data = _unitOfWork.CompanyRepo.GetChartData(companyId);
            return Json(data);
        }

        
        public IActionResult EditCompany(long companyId)
        {
            NewCompanyParams toReturn = _unitOfWork.TPORepo.EditCompany(companyId);
            return RedirectToAction("TPOLanding", "TPO", new
            {
                newCompanyParams = toReturn
            });
        }


    }
}
