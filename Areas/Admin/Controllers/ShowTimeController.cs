using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.Screen;
using BookMovieShow.DAL.ShowTime;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using DocumentFormat.OpenXml.EMMA;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ShowTimeController : Controller
    {
        ShowTimeDAL sTDAL = new ShowTimeDAL();

        private IConfiguration Configuration;
        public ShowTimeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult ShowTime_List(int CinemaID , int MovieID)
        {
            ViewBag.CinemaList = sTDAL.PR_Cinemas_ComboBox();
            ViewBag.MovieList = sTDAL.PR_Movies_ComboBox();
            ViewBag.ScreenList = sTDAL.PR_Screens_ComboBox();
            ViewBag.MovieList = sTDAL.PR_Movies_ComboBoxbyCinemaID(CinemaID);
            ViewBag.ScreenList = sTDAL.PR_Screens_ComboBoxbyCinemaIDAndMovieID(CinemaID,MovieID);
            DataTable dt = sTDAL.PR_ShowTimes_SelectAll();
            return View(dt);
        }
        #endregion

        #region ShowTime Save
        public IActionResult ShowTime_Save(ShowTimeModel showTimeModel)
        {
            if (ModelState.IsValid)
            {
                if (sTDAL.PR_ShowTimes_Insert(showTimeModel))
                {
                    TempData["Msg"] = "Record Inserted Successfully";
                    return RedirectToAction("ShowTime_List");
                }

            }
            TempData["Msg"] = "Record Inserted Error";
            return View("ShowTime_List");
        }
        #endregion

        #region Add ShowTime
        public IActionResult ShowTime_Add(int ShowTimeID,int CinemaID,int? MovieID)
        {
            ShowTimeModel showTimeModel = sTDAL.PR_ShowTimes_SelectByID(ShowTimeID);

            if (showTimeModel.ShowTimeID != null)
            {
                TempData["PageTitle"] = "ShowTime Edit Page";
                ViewBag.CinemaList = sTDAL.PR_Cinemas_ComboBox();
                ViewBag.MovieList = sTDAL.PR_Movies_ComboBox();
                ViewBag.ScreenList = sTDAL.PR_Screens_ComboBox();
                ViewBag.MovieList = sTDAL.PR_Movies_ComboBoxbyCinemaID(CinemaID);
                ViewBag.ScreenList = sTDAL.PR_Screens_ComboBoxbyCinemaIDAndMovieID(CinemaID, MovieID);
                return View("ShowTime_Add", showTimeModel);
            }
            else
            {
                TempData["PageTitle"] = "ShowTime Edit Page";
                ViewBag.CinemaList = sTDAL.PR_Cinemas_ComboBox();
                ViewBag.MovieList = sTDAL.PR_Movies_ComboBox();
                ViewBag.ScreenList = sTDAL.PR_Screens_ComboBox();
                //ViewBag.MovieList = sTDAL.PR_Movies_ComboBoxbyCinemaID(CinemaID);
                //ViewBag.ScreenList = sTDAL.PR_Screens_ComboBoxbyCinemaIDAndMovieID(CinemaID, MovieID);
                return View("ShowTime_Add");
            }
        }
        #endregion

        #region Delete ShowTime
        public IActionResult ShowTime_Delete(int ShowTimeID)
        {
            bool iSuccess = sTDAL.PR_ShowTimes_Delete(ShowTimeID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("ShowTime_List");

            }

            return RedirectToAction("ShowTime_List");
        }
        #endregion

        #region ShowTime Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    ShowTime_Delete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("ShowTime_List");
        }
        #endregion

        #region GetShowTimeModel
        public List<ShowTimeModel> GetShowTimeModel()
        {
            List<ShowTimeModel> models = new List<ShowTimeModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_ShowTimes_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ShowTimeModel model = new ShowTimeModel
                    {
                        ShowTimeID = Convert.ToInt32(reader["ShowtimeID"]),
                        CinemaName = reader["CinemaName"].ToString(),
                        Title = reader["Title"].ToString(),
                        ScreenName = reader["ScreenName"].ToString(),
                        ShowTime = Convert.ToDateTime(reader["ShowTime"]),
                        AvailableSeats = Convert.ToInt32(reader["AvailableSeats"]),
                        Price = Convert.ToInt32(reader["Price"]),
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

            List<ShowTimeModel> models = GetShowTimeModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("ShowTime");
                // Add headers
                worksheet.Cell(1, 1).Value = "ShowTimeID";
                worksheet.Cell(1, 2).Value = "CinemaName";
                worksheet.Cell(1, 3).Value = "MovieName";
                worksheet.Cell(1, 4).Value = "ScreenName";
                worksheet.Cell(1, 5).Value = "ShowTime";
                worksheet.Cell(1, 5).Value = "AvailableSeats";
                worksheet.Cell(1, 5).Value = "Price";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.ShowTimeID;
                    worksheet.Cell(row, 2).Value = model.CinemaName;
                    worksheet.Cell(row, 3).Value = model.Title;
                    worksheet.Cell(row, 4).Value = model.ScreenName;
                    worksheet.Cell(row, 5).Value = model.ShowTime;
                    worksheet.Cell(row, 5).Value = model.AvailableSeats;
                    worksheet.Cell(row, 5).Value = model.Price;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "ShowTimes.xlsx";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

        #endregion

        public IActionResult MovieDropDownByCinemaID(int CinemaID)
        {
            return ViewBag.MovieList = sTDAL.PR_Movies_ComboBoxbyCinemaID(CinemaID);
            
        }

        public IActionResult ScreenDropDownByCinemaIDAndMovieID(int MovieID,int CinemaID)
        {
            return ViewBag.ScreenList = sTDAL.PR_Screens_ComboBoxbyCinemaIDAndMovieID(CinemaID, MovieID);

        }

    }
}
