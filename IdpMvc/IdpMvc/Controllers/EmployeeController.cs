using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdpMvc.Controllers
{
    [Route("Employee")]
    public class EmployeeController : Controller
    {
        [HttpGet("get")]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
