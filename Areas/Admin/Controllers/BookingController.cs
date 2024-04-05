using BookMovieShow.Areas.Admin.Model;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using BookMovieShow.DAL.Admin.Booking;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class BookingController : Controller
    {
        Booking_DAL booking_DAL = new Booking_DAL();

        private IConfiguration Configuration;
        public BookingController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult Booking_List()
        {
            DataTable dt = booking_DAL.PR_Booking_SelectAll();
            return View(dt);
        }
        #endregion

        #region Booking Save
        public IActionResult Payment_Save(BookingModel bookingModel)
        {
            if (booking_DAL.PR_Booking_Insert(bookingModel))
            {
                TempData["Msg"] = "Record Inserted Successfully";
                return RedirectToAction("Booking_List");
            }

            TempData["Msg"] = "Record Inserted Error";
            return View("Booking_List");
        }
        #endregion

        #region Add Booking
        public IActionResult Booking_Add(int BookingID)
        {
            BookingModel bookingModel = booking_DAL.PR_Booking_SelectByID(BookingID);

            if (bookingModel.BookingID != null)
            {
                TempData["PageTitle"] = "Booking Edit Page";
                return View("Booking_Add", bookingModel);
            }
            else
            {
                TempData["PageTitle"] = "Booking Edit Page";
                return View("Booking_Add");
            }
        }
        #endregion

        #region Delete Booking
        public IActionResult Booking_Delete(int BookingID)
        {
            bool iSuccess = booking_DAL.PR_Booking_Delete(BookingID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("Booking_List");

            }

            return RedirectToAction("Booking_List");
        }
        #endregion

        #region Booking_Search
        public IActionResult Booking_Search(string? UserName)
        {

            DataTable dt = booking_DAL.PR_Booking_SearchByUserName(UserName);
            return View("Booking_List", dt);
        }
        #endregion

        #region Booking Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    Booking_Delete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("Booking_List");
        }
        #endregion

        #region GetBookingModel
        public List<BookingModel> GetBookingModel()
        {
            List<BookingModel> models = new List<BookingModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Booking_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    BookingModel model = new BookingModel
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ShowTimeID = Convert.ToInt32(reader["ShowTimeID"]),
                        NumberOfTickets = Convert.ToInt32(reader["NumberOfTickets"]),
                        SeatNumbers = reader["SeatNumbers"].ToString(),
                        TotalAmount = Convert.ToDecimal(reader["TotalAmount"]),
                        BookingDate = Convert.ToDateTime(reader["BookingDate"]),
                        BookingStatus = reader["BookingStatus"].ToString()
                    };
                    models.Add(model);
                }
                return models;
            }
        }

        #endregion

        #region ExportSToExcel
        public IActionResult ExportSToExcel()
        {

            List<BookingModel> models = GetBookingModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Booking");
                // Add headers
                worksheet.Cell(1, 1).Value = "BookingID";
                worksheet.Cell(1, 2).Value = "UserID";
                worksheet.Cell(1, 3).Value = "ShowTimeID";
                worksheet.Cell(1, 4).Value = "NumberOfTickets";
                worksheet.Cell(1, 5).Value = "SeatNumbers";
                worksheet.Cell(1, 6).Value = "TotalAmount";
                worksheet.Cell(1, 7).Value = "BookingDate";
                worksheet.Cell(1, 8).Value = "BookingStatus";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.BookingID;
                    worksheet.Cell(row, 2).Value = model.UserID;
                    worksheet.Cell(row, 3).Value = model.ShowTimeID;
                    worksheet.Cell(row, 4).Value = model.NumberOfTickets;
                    worksheet.Cell(row, 5).Value = model.SeatNumbers;
                    worksheet.Cell(row, 6).Value = model.TotalAmount;
                    worksheet.Cell(row, 7).Value = model.BookingDate;
                    worksheet.Cell(row, 8).Value = model.BookingStatus;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "Booking.xlsx";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

        #endregion
    }
}
