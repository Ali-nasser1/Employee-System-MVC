using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.BLL.Interfaces;
using Project.BLL.Repositories;
using Project.DAL.Models;
using Project.PL.ViewModels;

namespace Project.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRepository _departmentRepository, IMapper _mapper)
        {
            departmentRepository = _departmentRepository;
            mapper = _mapper;
        }
        public IActionResult Index()
        {
            var departments = departmentRepository.GetAll();
            var MappedDepartments = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDepartments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel department)
        {
            if(ModelState.IsValid)
            {
                var MappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                int Result = departmentRepository.Add(MappedDepartment);
                if(Result > 0)
                {
                    TempData["Message"] = "Department is created";  
                }
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
            var MappedDepartment = mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, MappedDepartment);
        }

        public IActionResult Edit(int? id) // retrieve data
        {
            return Details(id, "Edit");
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel department, [FromRoute] int id) // posting data
        {
            if (department.Id != id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                    departmentRepository.Update(MappedDepartment);
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
        public IActionResult Delete(DepartmentViewModel department,[FromRoute] int id)
        {
            if (department.Id != id) return BadRequest();
           if(ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                    departmentRepository.Delete(MappedDepartment);
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
