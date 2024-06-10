using Microsoft.AspNetCore.Mvc;
using Project.BLL.Interfaces;
using Project.DAL.Models;

namespace Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public IDepartmentRepository departmentRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository, IDepartmentRepository _departmentRepository)
        {
            employeeRepository = _employeeRepository;
            departmentRepository = _departmentRepository;
        }
        public IActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = departmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department = employeeRepository.GetById(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }

        public IActionResult Edit(int? id) // retrieve data
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(Employee employee, [FromRoute] int id) // posting data
        {
            if (employee.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (employee.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);
        }
    }
}
