using BookMovieShow.BAL;
using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
   
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class UserController : Controller
    {
        [Route("/User/User/Index")]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult User_DashBoard()
        {
            return View();
        }
    }
}
