using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Project.BLL.Interfaces;
using Project.DAL.Models;
using Project.PL.Utilities;
using Project.PL.ViewModels;

namespace Project.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly iUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController(iUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public IActionResult Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees = unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);
             
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
                employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");
               var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employee); // convert from X to Y the object Z

                unitOfWork.EmployeeRepository.Add(MappedEmployee);
                int Result = unitOfWork.Complete();
                if (Result > 0)
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
            var employee = unitOfWork.EmployeeRepository.GetById(id.Value);
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
                    employee.ImageName = DocumentSettings.UploadFile(employee.Image, "Images");
                    var MappedEmployee = mapper.Map<EmployeeViewModel, Employee>(employee);
                    unitOfWork.EmployeeRepository.Update(MappedEmployee);
                    unitOfWork.Complete();
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
                    unitOfWork.EmployeeRepository.Delete(MappedEmployee);
                    var Result = unitOfWork.Complete();
                    if(Result > 0 && employee.ImageName is not null)
                    {
                        DocumentSettings.DeleteFile(employee.ImageName, "Images");
                    }
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
