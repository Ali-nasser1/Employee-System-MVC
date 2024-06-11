using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.BLL.Interfaces;
using Project.DAL.Contexts;

namespace Project.BLL.Repositories
{
    public class UnitOfWork : iUnitOfWork
    {
        private readonly EmployeeSystemDbContext dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        public UnitOfWork(EmployeeSystemDbContext _dbContext)
        {
            EmployeeRepository = new EmployeeRepository(_dbContext);
            DepartmentRepository = new DepartmentRepository(_dbContext);
            dbContext = _dbContext;
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public int Complete()
        {
            return dbContext.SaveChanges();
        }
    }
}
