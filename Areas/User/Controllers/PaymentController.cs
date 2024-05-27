using BookMovieShow.Areas.User.Model;
using BookMovieShow.BAL;
using BookMovieShow.DAL.User.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class PaymentController : Controller
    {

        //[CheckAccess]
        public IActionResult Payment(PaymentModel model)
        {
            return View(model);
        }

    }
}
