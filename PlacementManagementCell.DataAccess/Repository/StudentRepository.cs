using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PlacementManagementCell.DataAccess.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly PlacementManagementContext _db;

        public StudentRepository(PlacementManagementContext db)
        {
            _db = db;
        }

        public Student getStudentByEmail(string Email)
        {
            var getUser = _db.Students.Where(s => s.EmailAddress.Equals(Email)).FirstOrDefault();
            if (getUser == null) return null;
            return getUser;
        }
        public Student getStudentByErNo(string erNo)
        {
            var getUser = _db.Students.Where(s => s.EnrollmentNumber== erNo.ToString()).FirstOrDefault();
            return getUser;
        }

        public Student getStudentByToken(string token)
        {
            Student std = _db.Students.Where(s => s.Token.Equals(token)).FirstOrDefault();
            return std;
        }
        public Student LoginStudent(string EmailAddress, string Password)
        {
            Student checkStudent = _db.Students.Where(s => s.EmailAddress==EmailAddress).FirstOrDefault();
            if (checkStudent != null && checkStudent.Password.Equals(Password) && checkStudent.IsVerified==true  /* && BCrypt.Net.BCrypt.Verify(Password,checkStudent.Password)*/)
            {
                return checkStudent;
            }
            return null;
        }

        public bool RegisterStudent(Student student)
         {
            var studentInMajor = _db.Studentmajors.FirstOrDefault(s => s.EnrollmentNo.Equals(student.EnrollmentNumber));
            var checkIfMultipleReg = _db.Students.FirstOrDefault(s => s.EnrollmentNumber.Equals(student.EnrollmentNumber));
            if (studentInMajor != null && checkIfMultipleReg==null)
            {
                string token = Guid.NewGuid().ToString().Substring(0, 6);
                studentInMajor.Token = token;
                _db.Update(studentInMajor);

                var subject = "Verification OTP by Placement Management Cell";
                var body = "Hello, " + student.FirstName + " " + student.LastName + ", Your OTP for registration is : " + token;
                var email = student.EmailAddress;
                SendEmail(email, body,subject);
                _db.Students.Add(student);
                return true;
            }
            return false;
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

        public void Save()
        {
            _db.SaveChanges();
        }

        public void storeTokem(Student student, string token)
        {
            student.Token = token;
        }

        public void UpdateStudent(Student student,string token)
        {
            student.Token = token;
            student.TokenCreatedAt = DateTime.Now;
            _db.Students.Update(student);
        }

        public void changePassword  (Student student)
        {
            _db.Update(student);
        }
        public List<Company> getAppliedCompanies(string enrollmentNo)
        {
            var companies = _db.CompanyApplications
            .Where(ca => ca.EnrollmentNo == enrollmentNo)
            .Select(ca => ca.Company)
            .ToList();

            return companies;
        }
    }
}
