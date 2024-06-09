using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.BLL.Interfaces;
using Project.DAL.Contexts;
using Project.DAL.Models;

namespace Project.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeSystemDbContext dbContext;

        public EmployeeRepository(EmployeeSystemDbContext _dbContext):base(_dbContext)
        {
            dbContext = _dbContext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return dbContext.employees.Where(E => E.Address == address);
        }
    }
}
