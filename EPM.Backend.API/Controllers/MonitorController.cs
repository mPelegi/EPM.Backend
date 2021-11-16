using EPM.Backend.Model.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EPM.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : Controller
    {
        [HttpGet]
        [Route("description")]
        [DisableRequestSizeLimit]
        public IActionResult Description([FromBody] DescriptionDTO description)
        {
            // ip/api/monitor
            IActionResult response = Unauthorized();

            try
            {
                response = Ok("Descriptions received successfully!");
            }
            catch (Exception ex)
            {
                response = StatusCode(500, ex.Message);
            }

            return response;
        }
    }
}
