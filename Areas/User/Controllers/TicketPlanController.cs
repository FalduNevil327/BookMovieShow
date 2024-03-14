using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class TicketPlanController : Controller
    {
        public IActionResult TicketPlan()
        {
            return View();
        }
    }
}
