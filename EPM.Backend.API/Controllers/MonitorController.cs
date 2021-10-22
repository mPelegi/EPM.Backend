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
        [HttpPost]
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

        [HttpGet]
        [Route("description/{id}")]
        [DisableRequestSizeLimit]
        public IActionResult GetDescription(int id)
        {
            // ip/api/monitor
            IActionResult response = Unauthorized();

            try
            {
                response = Ok();
            }
            catch (Exception ex)
            {
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [Route("performance")]
        [DisableRequestSizeLimit]
        public IActionResult Performance([FromBody] PerformanceDTO performance)
        {
            // ip/api/monitor
            IActionResult response = Unauthorized();

            try
            {
                response = Ok("Performance received successfully!");
                
            }
            catch (Exception ex)
            {
                response = StatusCode(500, ex.Message);
            }

            return response;
        }

        [HttpPost]
        [Route("performance/{id}")]
        [DisableRequestSizeLimit]
        public IActionResult GetPerformance(int id)
        {
            // ip/api/monitor
            IActionResult response = Unauthorized();

            try
            {
                response = Ok();

            }
            catch (Exception ex)
            {
                response = StatusCode(500, ex.Message);
            }

            return response;
        }
    }
}
