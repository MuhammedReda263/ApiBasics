using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.DTO;
using WebApiProject.Models;
using WebApiProject.Repository;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController] // it's for Binding (premetive in url , complex in body) , For validation too expend ModelState
    public class DepartmentController : ControllerBase
    {
        private IDepartment repositery;
        public DepartmentController(IDepartment repo)
        {
            repositery = repo;
        }
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            List<Department> departments = repositery.GetAllDepartments();
            return Ok(departments);
        }
        [HttpGet("EmpCount")]
        public IActionResult GetAllDepartmentsWithEmps()
        {
            List<DeptWithEmpCountDto>  deptWithEmpCountDtos = new List<DeptWithEmpCountDto>();
            List<Department> departments = repositery.GetAllDepartmentsWithEmps();
            foreach (Department item in departments)
            {
                DeptWithEmpCountDto Dto = new DeptWithEmpCountDto();
                Dto.Name = item.Name;
                Dto.MangerName = item.MangerName;
                Dto.Count = item.Employees.Count();
                deptWithEmpCountDtos.Add(Dto);
            }
            return Ok(deptWithEmpCountDtos);
        }


        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetDepartmentById(int id)
        {
            Department department = repositery.GetDepartmentById(id);
            return Ok(department);
        }
        [HttpGet("{name:alpha}")]
        public ActionResult <GeneralRespone> GetDepartmentByName(string name)
        {
            Department department = repositery.GetDepartmentByName(name);
            GeneralRespone generalRespone = new GeneralRespone();
            if (department == null)
            {
                generalRespone.Success = false;
                generalRespone.data = null; 
                return generalRespone;
            }
            generalRespone.Success = true;
            generalRespone.data = department;
            return generalRespone;
        }

        [HttpPost]
        public IActionResult AddDepartment(Department department)
        {
            repositery.AddDepartment(department);
            repositery.SaveChanges();
            return CreatedAtAction(nameof(GetDepartmentById), new { id = department.Id }, department);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment(int id, Department department)
        {
            var updated = repositery.UpdateDepartment(id, department);
            if (updated == null)
            {
                return NotFound($"Department with ID {id} not found.");
            }
            repositery.SaveChanges();
            return Ok(updated);


        }


    }
}