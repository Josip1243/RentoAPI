using Microsoft.AspNetCore.Mvc;

namespace Rento.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [HttpGet("/error")]
        public IActionResult Error()
        {
            return Problem();
        }
    }
}
