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
        public async Task Add(T item)
        =>  await dbContext.AddAsync(item);
        public void Delete(T item)
         => dbContext.Remove(item);

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await dbContext.employees.Include(E => E.Department).ToListAsync();
            }

            return await dbContext.Set<T>().ToListAsync();
            
        }

        public async Task<T> GetById(int id)
          => await dbContext.Set<T>().FindAsync(id);
        

        public void Update(T item)
         => dbContext.Set<T>().Update(item);
    }
}
