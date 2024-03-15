using BookMovieShow.BAL;
using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class SeatPlanController : Controller
    {
       
        public IActionResult SeatPlan()
        {
            return View();
        }
    }
}
