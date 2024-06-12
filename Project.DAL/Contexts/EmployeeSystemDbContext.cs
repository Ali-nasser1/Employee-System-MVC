using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.DAL.Models;

namespace Project.DAL.Contexts
{
    public class EmployeeSystemDbContext : IdentityDbContext<ApplicationUser>
    {
        public EmployeeSystemDbContext(DbContextOptions<EmployeeSystemDbContext> options) : base(options)
        { }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // => optionsBuilder.UseSqlServer("Server = . ; Database = EmployeeSystemDb ; Trusted_Connection = true;");
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
