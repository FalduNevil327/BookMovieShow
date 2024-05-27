using BookMovieShow.Areas.User.Model;
using BookMovieShow.DAL.User.BookingDetail;
using BookMovieShow.DAL.User.SeatPlan;
using DocumentFormat.OpenXml.EMMA;
using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class BookingDetailController : Controller
    {
        BookingDetailDAL bDAL = new BookingDetailDAL();
        //public IActionResult BookingDetail(int BookingID, int ShowtimeID)
        //{
        //    BookingDetailModel model = bDAL.PR_Bookings_GetDetails(BookingID, ShowtimeID);
        //    return View("BookingDetail", model);
        //}
        //[HttpPost]
        //public ActionResult ConfirmSeats(BookingDetailModel model)
        //{
            

        //    // Store the booking details in session to pass it to the next action
        //    //Session["BookingDetailsModel"] = bookingDetails;

        //    return RedirectToAction("BookingDetail" ,bookingDetails);
        //}

        // Action to display the payment page
        public ActionResult BookingDetail(SeatPlanModel model)
        {
            var bookingDetails = new SeatPlanModel
            {
                Title = model.Title,
                Language = model.Language,
                Genre = model.Genre,
                CinemaName = model.CinemaName,
                //NumberOfTickets = model.NumberOfTickets,
                //BookingDate = model.BookingDate,
                //Showtime = model.Showtime,
                //price = model.price,
                //SeatNumbers = model.SeatNumbers,
                //TotalAmount = model.TotalAmount
            };

            return View("BookingDetail", bookingDetails);
        }
    }
}
