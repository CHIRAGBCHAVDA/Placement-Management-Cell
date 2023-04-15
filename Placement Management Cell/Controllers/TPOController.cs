using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel.Resolution;
using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using PlacementManagementCell.Models.ViewModels;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Globalization;
using System.Net.Mail;
using System.Text;

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

        public IActionResult TPOLanding()
        {
            return View();
        }

        [HttpPost]
        public IActionResult NewCompany(NewCompanyParams companyParams)
        {
            var viewModel = _unitOfWork.TPORepo.AddNewCompany(companyParams);
            return Json(viewModel);
        }

        public IActionResult TPOAllCompanyDashboard(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            TPOCompanyCardTotal viewModel = _unitOfWork.CompanyRepo.getTPOCompaniesCard(searchKeyword, pageNum, sortBy);
            return View(viewModel);
        }

        [HttpPost]
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
    }

}
