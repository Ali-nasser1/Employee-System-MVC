using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Project.BLL.Interfaces;
using Project.DAL.Contexts;
using Project.DAL.Models;

namespace Project.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EmployeeSystemDbContext dbContext;

        public GenericRepository(EmployeeSystemDbContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public int Add(T item)
        {
            dbContext.Add(item);
            return dbContext.SaveChanges();
        }

        public int Delete(T item)
        {
            dbContext.Remove(item);
            return dbContext.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>)dbContext.employees.Include(E => E.Department).ToList();
            }

            return dbContext.Set<T>().ToList();
            
        }

        public T GetById(int id)
        {
            return dbContext.Set<T>().Find(id);
        }

        public int Update(T item)
        {
            dbContext.Set<T>().Update(item);
            return dbContext.SaveChanges();
        }
    }
}
