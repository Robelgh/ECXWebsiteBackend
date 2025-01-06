using Microsoft.AspNetCore.Mvc;

namespace ECX.Website.API.Controllers
{
    public class RequestInfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
