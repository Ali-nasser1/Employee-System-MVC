using Project.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Project.PL.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "the name is too long max is 50")]
        [MinLength(5, ErrorMessage = "the name is too small min is 5")]
        public string Name { get; set; }

        [Range(18, 35, ErrorMessage = "Age must be between (18, 35)")]
        public int? Age { get; set; }
        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{3,10}$",
            ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        public string ImageName { get; set; }
        public IFormFile Image { get; set; }

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        //[InverseProperty(nameof(Employee))]
        public Department Department { get; set; }
    }
}
