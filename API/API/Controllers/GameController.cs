using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GameController : BaseApiController
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Hello world!");
        }
    }
}
