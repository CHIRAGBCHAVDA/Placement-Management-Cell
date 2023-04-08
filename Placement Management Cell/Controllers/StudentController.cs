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
using Placement_Management_Cell.Authorization;
using PlacementManagementCell.Models.ViewModels;

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
                var returnedByMethod = _unitOfWork.StudentRepo.RegisterStudent(student);
                if (returnedByMethod == true)
                {
                    _unitOfWork.StudentRepo.Save();
                    TempData["Verification-mail"] = "Verification mail is sent on your email address!!";
                    return RedirectToAction("StudentVerification", "Student");
                }
                else
                {
                    TempData["reg-fail"] = "Could not register the outsider student or student already exists...!";
                    return RedirectToAction("Registration", "Student");
                }
            }
            catch(DataException)
            {
                return RedirectToAction("StudentVerification", "Student");
            }
        }

        [HttpPost]
        public ActionResult StudentLogin(string EmailAddress, string Password)
        {
            //string pwd = BCrypt.Net.BCrypt.HashPassword(Password);
            var checkStudent = _unitOfWork.StudentRepo.LoginStudent(EmailAddress, Password);
            if (checkStudent != null)
            {
                TempData["user-login-success"] = "User Logged in Successfully";
                HttpContext.Session.SetString("role", "student");
                HttpContext.Session.SetString("erNo", checkStudent.EnrollmentNumber);
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
            if (getStudentByToken != null && (getStudentByToken).TokenCreatedAt >= DateTime.Now.AddMinutes(-5))
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

        [AuthAttribute]
        public IActionResult StudentLanding(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            string erNo = HttpContext.Session.GetString("erNo");
            var student = _unitOfWork.StudentRepo.getStudentByErNo(erNo);
            HttpContext.Session.SetString("BranchId", student.BranchId.ToString());
            var companyCards = _unitOfWork.CompanyRepo.getCompanyCards(searchKeyword, pageNum, sortBy,student.BranchId);
            StudentHeaderViewModel StudentHeader = new StudentHeaderViewModel()
            {
                EnrollmentNo = erNo,
                Avatar = student.Avatar,
                Name = student.FirstName + " " + student.LastName
            };

            StudentCompanyViewModel StudentCompany = new StudentCompanyViewModel()
            {
                StudentHeader = StudentHeader,
                CompanyCardsTotal = companyCards
            };

            return View(StudentCompany);
        }


        public IActionResult StudentApplication(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy)
        {
            var enrollmentno = HttpContext.Session.GetString("erNo");
            var student = _unitOfWork.StudentRepo.getStudentByErNo(enrollmentno);
            var companies = _unitOfWork.CompanyRepo.getCompanyApplicationCards(enrollmentno,searchKeyword,pageNum,sortBy);

            StudentHeaderViewModel StudentHeader = new StudentHeaderViewModel()
            {
                EnrollmentNo = enrollmentno,
                Avatar = student.Avatar,
                Name = student.FirstName + " " + student.LastName
            };
            

            StudentCompanyViewModel StudentCompany = new StudentCompanyViewModel()
            {
                StudentHeader = StudentHeader,
                CompanyCardsTotal = companies
            };
            return View(StudentCompany);
        }

        [HttpPost]
        public IActionResult CompanyFilter(string? searchKeyword, [DefaultValue(1)] int pageNum, [DefaultValue(1)] int sortBy,bool fromApplications)
        {
            var comp = new CompanyCardsTotalViewModel();
            var enrollmentno = HttpContext.Session.GetString("erNo");
            if (fromApplications)
            {
                comp = _unitOfWork.CompanyRepo.getCompanyApplicationCards(HttpContext.Session.GetString("erNo"), searchKeyword, pageNum, sortBy);
            }
            else
            {
            comp = _unitOfWork.CompanyRepo.getCompanyCards(searchKeyword, pageNum, sortBy,int.Parse(HttpContext.Session.GetString("BranchId")));
            }
            return PartialView("_CompanyCardList",comp);
        }

        [AuthAttribute]
        public IActionResult CompanyDetail(long companyId)
        {
            var student = _unitOfWork.StudentRepo.getStudentByErNo(HttpContext.Session.GetString("erNo"));
            StudentHeaderViewModel StudentHeader = new StudentHeaderViewModel()
            {
                EnrollmentNo = student.EnrollmentNumber,
                Avatar = student.Avatar,
                Name = student.FirstName + " " + student.LastName
            };
            var company = _unitOfWork.CompanyRepo.getCompanyById(companyId);
            StudentCompanyDetailViewModel StudentCompany = new StudentCompanyDetailViewModel()
            {
                Company = company,
                StudentHeader = StudentHeader
            };

            return View(StudentCompany);
        }

        public IActionResult StudentVerification()
        {
            TempData["mail-success"] = "Reset password link has been sent to your email id.";
            return View();
        }

        [HttpPost]
        public IActionResult StudentVerification(StudentVerificationViewModel studentVerificationViewModel)
        {
            var std = _db.Studentmajors.FirstOrDefault(student => student.EnrollmentNo == studentVerificationViewModel.EnrollmentNo && student.Token == studentVerificationViewModel.OTP);
            if (std != null)
            {
                var student = _db.Students.FirstOrDefault(s => s.EnrollmentNumber == studentVerificationViewModel.EnrollmentNo);
                student.IsVerified = true;
                _db.Update(student);
                _db.SaveChanges();
                TempData["reg-fail"] = "Student Verified and Registered successfully...!";
                return RedirectToAction("Login", "Student");
            }
            else
            {
                TempData["otp-wrong"] = "The OTP has been provided is incorrect..!!";
                return RedirectToAction("StudentVerification", "Student");
            }
        }

        [HttpPost]
        public IActionResult ApplyCompany(int companyId)
        {
            var BaseResponseModel = _unitOfWork.CompanyRepo.ApplyCompanyById(companyId, HttpContext.Session.GetString("erNo"));
            TempData["company-apply-success"] = "Your application is submitted";
            return RedirectToAction("CompanyDetail", "Student",companyId);
        }
    }
}