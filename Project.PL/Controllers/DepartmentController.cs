using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.BLL.Interfaces;
using Project.BLL.Repositories;
using Project.DAL.Models;

namespace Project.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        public DepartmentController(IDepartmentRepository _departmentRepository)
        {
            departmentRepository = _departmentRepository;
        }
        public IActionResult Index()
        {
            var departments = departmentRepository.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if(ModelState.IsValid)
            {
                departmentRepository.Add(department);
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department = departmentRepository.GetById(id.Value);
            if(department is null)
                return NotFound();
            return View(ViewName,department);
        }

        public IActionResult Edit(int? id) // retrieve data
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(Department department, [FromRoute] int id) // posting data
        {
            if (department.Id != id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    departmentRepository.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public IActionResult Delete(int? id) 
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Department department,[FromRoute] int id)
        {
            if (department.Id != id) return BadRequest();
           if(ModelState.IsValid)
            {
                try
                {
                    departmentRepository.Delete(department);
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }
    }
}
