using FrontendBlazor.Helpers;
using FrontendBlazor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FrontendBlazor.Controllers
{
    [SecurityModel]
    public class ScrapController : Controller
    {
        private ScrapHelper scrapHelper;

        public ScrapController()
        {
            scrapHelper = new ScrapHelper();

        }

        [HttpGet("GetPolicyInfoMessage")]
        public async Task<IActionResult> GetPolicyInfoMessage(string id)
        {
            try
            {
                string userId = HttpContext.Session.Get("Email").ToString();
           
                var message = await scrapHelper.GetPolicyInfoMessage(id, userId);
                return Ok(new { message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = $"Error al obtener el mensaje de la póliza: {ex.Message}" });
            }
        }



    }
}
