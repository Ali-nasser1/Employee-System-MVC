using Project.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Project.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "the name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "the code is required")]
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
