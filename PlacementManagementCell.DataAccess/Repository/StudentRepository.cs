using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using PlacementManagementCell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            if (checkStudent != null && checkStudent.Password.Equals(Password)  /* && BCrypt.Net.BCrypt.Verify(Password,checkStudent.Password)*/)
            {
                return checkStudent;
            }
            return null;
        }

        public void RegisterStudent(Student student)
        {
            _db.Students.Add(student);
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
    }
}
