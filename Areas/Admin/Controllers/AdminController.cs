using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminController : Controller
    {
 
        public IActionResult Admin_DashBoard()
        {
            return View();
        }
    }
}
