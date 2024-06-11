using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Interfaces;
using Project.DAL.Models;
using Project.PL.ViewModels;

namespace Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRepository _employeeRepository, IDepartmentRepository _departmentRepository, IMapper _mapper)
        {
            employeeRepository = _employeeRepository;
            departmentRepository = _departmentRepository;
            mapper = _mapper;
        }
        public IActionResult Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees = employeeRepository.GetAll();
            else
                employees = employeeRepository.GetEmployeesByName(SearchValue);
             
            var MappedEmployees = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
                return View(MappedEmployees);

        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
               var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employee); // convert from X to Y the object Z

               var Result = employeeRepository.Add(MappedEmployee);
                if(Result > 0)
                {
                    TempData["Message"] = "The Employee is created";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee = employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
            var MappedEmployee = mapper.Map<Employee, EmployeeViewModel>(employee);
            return View(ViewName, MappedEmployee);
        }

        public IActionResult Edit(int? id) // retrieve data
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employee, [FromRoute] int id) // posting data
        {
            if (employee.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employee);
                    employeeRepository.Update(MappedEmployee);
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
        public IActionResult Delete(EmployeeViewModel employee, [FromRoute] int id)
        {
            if (employee.Id != id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employee);
                    employeeRepository.Delete(MappedEmployee);
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
