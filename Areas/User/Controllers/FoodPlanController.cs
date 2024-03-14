using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class FoodPlanController : Controller
    {
        public IActionResult FoodPlanList()
        {
            return View();
        }
    }
}
