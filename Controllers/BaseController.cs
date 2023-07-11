using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController()]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        protected readonly IWebHostEnvironment HostEnvironment;
        protected readonly ILogger Logger;

        public BaseController(IWebHostEnvironment hostEnvironment, ILogger logger)
        {
            HostEnvironment = hostEnvironment;
            Logger = logger;
        }
    }
}
