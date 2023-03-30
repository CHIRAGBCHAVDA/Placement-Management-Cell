using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;
using System.Web.Helpers;
using System.ComponentModel;

namespace Placement_Management_Cell.Controllers
{
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly PlacementManagementContext _db;

        public StudentController(ILogger<StudentController> logger, IUnitOfWork unitOfWork, PlacementManagementContext db)
        {
            _logger = logger;
            _db = db;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        public IActionResult LostPassword()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        public ActionResult StudentRegistrationPOST(Student student)
        {
            try
            {
                //student.Password = BCrypt.Net.BCrypt.HashPassword(student.Password);
                _unitOfWork.StudentRepo.RegisterStudent(student);
                _unitOfWork.StudentRepo.Save();
                TempData["reg-success"] = "User Registered Successfully!";
            }
            catch(DataException)
            {
                TempData["reg-fail"] = "Could not save the user...!";
            }
            return RedirectToAction("Login", "Student");
        }

        [HttpPost]
        public ActionResult StudentLogin(string EmailAddress, string Password)
        {
            //string pwd = BCrypt.Net.BCrypt.HashPassword(Password);
            var checkStudent = _unitOfWork.StudentRepo.LoginStudent(EmailAddress, Password);
            if (checkStudent != null)
            {
                TempData["user-login-success"] = "User Logged in Successfully";
                return RedirectToAction("StudentLanding", "Student");
            }
            TempData["user-login-fail"] = "Error Occurred While Login, Check the credentials...!";
            return RedirectToAction("Login", "Student");
        }

        [HttpPost]
        public IActionResult LostPasswordReq(string EmailAddress)
        {
            var getUser = _unitOfWork.StudentRepo.getStudentByEmail(EmailAddress);
            if (getUser != null)
            {
                string resetcode = Guid.NewGuid().ToString();
                var verifyUrl = "/Student/ForgotPassword/" + resetcode;
                var link = HttpContext.Request.GetDisplayUrl().Replace(HttpContext.Request.Path, verifyUrl);

                _unitOfWork.StudentRepo.UpdateStudent(getUser, resetcode);
                _unitOfWork.Save();

                var subject = "Password Reset Request";
                var body = "Hi " + getUser.FirstName + ", <br/> You recently requested to reset your password for your account. Click the link below to reset it. " + " <br/><br/><a href='" + link + "'>" + link + "</a> <br/><br/>" + "If you did not request a password reset, please ignore this email or reply to let us know.<br/><br/> Thank you";
                SendEmail(getUser.EmailAddress, body, subject);
                TempData["mail-success"] = "Reset password link has been sent to your email id.";
                return RedirectToAction("LostPassword","Student");
            }
            else
            {
                TempData["user-not-exists"] = "User doesn't exists.";
                return View();
            }
        }

        [HttpPost]
        public IActionResult ForgotPassword(Student student)
        {
            string token = HttpContext.Request.GetDisplayUrl().Replace("https://localhost:44357/Student/ForgotPassword/", "");
            Student getStudentByToken = _unitOfWork.StudentRepo.getStudentByToken(token);
            if (getStudentByToken != null && (getStudentByToken).TokenCreatedAt >= DateTime.Now.AddMinutes(-1))
            {
                //string pwd = BCrypt.Net.BCrypt.HashPassword(student.Password);
                //getStudentByToken.Password = pwd;
                string pwd = student.Password;
                getStudentByToken.Password = pwd;
                _unitOfWork.StudentRepo.changePassword(getStudentByToken);
                _unitOfWork.Save();
                TempData["reset-success"] = "Your password has been updated !";
                return RedirectToAction("Login","Student");
            }
            else
            {
                TempData["reset-fail"] = "Link is not valid or The link is expired, Generate the new one ...!";
                return View();
            }
        }



        public void SendEmail(string email, string body, string subject)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("chiragchavda.tatvasoft@gmail.com", "orltrydyhfxgxdrz");
            client.EnableSsl = true;

            var message = new MailMessage();
            message.From = new MailAddress("chiragchavda.tatvasoft@gmail.com");
            message.To.Add(email);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            client.Send(message);

        }
        public IActionResult StudentLanding(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {                                   
            var comp = _unitOfWork.CompanyRepo.getCompanyCards(searchKeyword, pageNum, sortBy);
            return View(comp);
        }

        [HttpPost]
        public IActionResult CompanyFilter(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            var comp = _unitOfWork.CompanyRepo.getCompanyCards(searchKeyword, pageNum, sortBy);
            return PartialView("_CompanyCardList",comp);
        }
    }
}