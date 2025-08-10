using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BindingController : ControllerBase
    {
        [HttpGet ("{age}/{name}")]
        public IActionResult testPremative(int age, string name)
        {
            return Ok();
        }


        [HttpPost]
        public IActionResult TestObj(Department department, string name)
        {
            return Ok(department);
        }
        

        [HttpGet("{Id}/{Name}/{MangerName}")]
        public IActionResult TestCustomBind([FromRoute]Department department)
        {
            return Ok(department);
        }
    }
}
