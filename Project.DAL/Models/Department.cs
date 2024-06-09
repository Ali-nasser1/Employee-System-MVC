using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "the name is required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "the code is required")]
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
