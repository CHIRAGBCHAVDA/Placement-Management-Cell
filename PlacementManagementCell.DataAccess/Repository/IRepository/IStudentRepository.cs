using PlacementManagementCell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository.IRepository
{
    public interface IStudentRepository
    {
        bool RegisterStudent(Student student);
        Student LoginStudent(string Email, string Password);
        void UpdateStudent(Student student,string token);
        void Save();

        Student getStudentByEmail(string Email);
        void storeTokem(Student student, string token);
        Student getStudentByToken(string token);
        public void changePassword(Student student);
        public Student getStudentByErNo(string erNo);
    }
}
