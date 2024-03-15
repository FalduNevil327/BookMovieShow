using BookMovieShow.BAL;
using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class PaymentController : Controller
    {
        [CheckAccess]
        public IActionResult Payment()
        {
            return View();
        }
    }
}
