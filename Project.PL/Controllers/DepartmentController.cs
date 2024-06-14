using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Project.BLL.Interfaces;
using Project.BLL.Repositories;
using Project.DAL.Models;
using Project.PL.ViewModels;

namespace Project.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly iUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(iUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAll();
            var MappedDepartments = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            return View(MappedDepartments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if(ModelState.IsValid)
            {
                var MappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                await unitOfWork.DepartmentRepository.Add(MappedDepartment);
                int Result = await unitOfWork.Complete();
                if(Result > 0)
                {
                    TempData["Message"] = "Department is created";  
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var department = await unitOfWork.DepartmentRepository.GetById(id.Value);
            if(department is null)
                return NotFound();
            var MappedDepartment = mapper.Map<Department, DepartmentViewModel>(department);
            return View(ViewName, MappedDepartment);
        }

        public async Task<IActionResult> Edit(int? id) // retrieve data
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel department, [FromRoute] int id) // posting data
        {
            if (department.Id != id) return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                    unitOfWork.DepartmentRepository.Update(MappedDepartment);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Delete(int? id) 
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel department,[FromRoute] int id)
        {
            if (department.Id != id) return BadRequest();
           if(ModelState.IsValid)
            {
                try
                {
                    var MappedDepartment = mapper.Map<DepartmentViewModel, Department>(department);
                    unitOfWork.DepartmentRepository.Delete(MappedDepartment);
                    await unitOfWork.Complete();
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
