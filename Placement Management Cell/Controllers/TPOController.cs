using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;
using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models.ViewModels;

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

        public IActionResult TPOAllCompanyDashboard()
        {
            TPOCompanyCardTotal viewModel = _unitOfWork.CompanyRepo.getTPOCompaniesCard();
            return View(viewModel);
        }
    }

}
