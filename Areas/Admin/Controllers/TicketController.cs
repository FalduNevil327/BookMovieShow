using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.Ticket;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class TicketController : Controller
    {
        Ticket_DAL ticket_DAL = new Ticket_DAL();

        private IConfiguration Configuration;
        public TicketController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult Ticket_List()
        {
            DataTable dt = ticket_DAL.PR_Ticket_SelectAll();
            return View(dt);
        }
        #endregion

        #region Ticket Save
        public IActionResult Ticket_Save(TicketModel ticketModel)
        {
            if (ticket_DAL.PR_Tickets_Insert(ticketModel))
            {
                TempData["Msg"] = "Record Inserted Successfully";
                return RedirectToAction("Ticket_List");
            }

            TempData["Msg"] = "Record Inserted Error";
            return View("Ticket_List");
        }
        #endregion

        #region Add Ticket
        public IActionResult Ticket_Add(int TicketID)
        {
            TicketModel ticketModel = ticket_DAL.PR_Tickets_SelectByID(TicketID);

            if (ticketModel.TicketID != null)
            {
                TempData["PageTitle"] = "Ticket Edit Page";
                return View("Ticket_Add", ticketModel);
            }
            else
            {
                TempData["PageTitle"] = "Ticket Edit Page";
                return View("Ticket_Add");
            }
        }
        #endregion

        #region Delete Ticket Details
        public IActionResult Ticket_Delete(int TicketID)
        {
            bool iSuccess = ticket_DAL.PR_Tickets_Delete(TicketID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("Ticket_List");

            }

            return RedirectToAction("Ticket_List");
        }
        #endregion

        #region Ticket_Search By UserName
        public IActionResult Ticket_Search(string? UserName)
        {

            DataTable dt = ticket_DAL.PR_Tickets_SearchByUserName(UserName);
            return View("Ticket_List", dt);
        }
        #endregion

        #region Ticket Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    Ticket_Delete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("Ticket_List");
        }
        #endregion

        #region GetTicketModel
        public List<TicketModel> GetTicketModel()
        {
            List<TicketModel> models = new List<TicketModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Tickets_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    TicketModel model = new TicketModel
                    {
                        BookingID = Convert.ToInt32(reader["BookingID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ShowTimeID = Convert.ToInt32(reader["ShowTimeID"]),
                        SeatNumbers = reader["SeatNumbers"].ToString(),
                        QRCode = reader["QRCode"].ToString(),
                        TicketMode = reader["TicketMode"].ToString(),
                        TicketStatus = reader["TicketStatus"].ToString()
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

            List<TicketModel> models = GetTicketModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Ticket");
                // Add headers
                worksheet.Cell(1, 1).Value = "BookingID";
                worksheet.Cell(1, 2).Value = "UserID";
                worksheet.Cell(1, 3).Value = "ShowTimeID";
                worksheet.Cell(1, 4).Value = "SeatNumbers";
                worksheet.Cell(1, 5).Value = "QRCode";
                worksheet.Cell(1, 6).Value = "TicketMode";
                worksheet.Cell(1, 7).Value = "TicketStatus";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.BookingID;
                    worksheet.Cell(row, 2).Value = model.UserID;
                    worksheet.Cell(row, 3).Value = model.ShowTimeID;
                    worksheet.Cell(row, 4).Value = model.SeatNumbers;
                    worksheet.Cell(row, 5).Value = model.QRCode;
                    worksheet.Cell(row, 6).Value = model.TicketMode;
                    worksheet.Cell(row, 7).Value = model.TicketStatus;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "Ticket.xlsx";
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
