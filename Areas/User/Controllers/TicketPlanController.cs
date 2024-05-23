using BookMovieShow.Areas.User.Model;
using BookMovieShow.DAL.User.TicketPlan;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class TicketPlanController : Controller
    {
        TicketPlan_DAL tDAL = new TicketPlan_DAL();
        public IActionResult TicketPlan(int MovieID)
            {
            List<TicketPlanModel> list = tDAL.PR_Showtimes_ByMovieID(MovieID);
            return View(list);
        }
    }
}
