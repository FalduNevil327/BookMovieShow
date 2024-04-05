using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.User.Model;
using BookMovieShow.BAL;
using BookMovieShow.DAL.User.MovieDetail;
using BookMovieShow.DAL.User.SeatPlan;
using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class SeatPlanController : Controller
    {
        SeatPlan_DAL seatPlan_DAL = new SeatPlan_DAL();

        #region SeatPlan
        public IActionResult SeatPlan(int ShowTimeID)
        {
            SeatPlanModel model = seatPlan_DAL.PR_Showtimes_ForSeatPlan(ShowTimeID);
            return View("SeatPlan", model);

        }
        #endregion
    }
}
