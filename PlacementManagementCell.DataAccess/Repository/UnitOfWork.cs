using PlacementManagementCell.DataAccess.Data;
using PlacementManagementCell.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementManagementCell.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PlacementManagementContext _db;

        public UnitOfWork(PlacementManagementContext db)
        {
            _db = db;
            StudentRepo = new StudentRepository(_db);
            CompanyRepo = new CompanyRepository(_db);
        }


        public IStudentRepository StudentRepo { get; }
        public ICompanyRepository CompanyRepo { get; }


        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
