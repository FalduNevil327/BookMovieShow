using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.Screen;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ScreenController : Controller
    {

        ScreenDAL screenDAL = new ScreenDAL();

        private IConfiguration Configuration;
        public ScreenController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult ScreenList()
        {
            ViewBag.CinemaList = screenDAL.PR_Cinemas_ComboBox();
            ViewBag.MovieList = screenDAL.PR_Movies_ComboBox();
            DataTable dt = screenDAL.PR_Cinemas_SelectAll();
            return View(dt);
        }
        #endregion

        #region Screen Save
        public IActionResult ScreenSave(ScreenModel screenModel)
        {
            if (ModelState.IsValid)
            {
                if (screenDAL.PR_Screens_Insert(screenModel))
                {
                    TempData["Msg"] = "Record Inserted Successfully";
                    return RedirectToAction("ScreenList");
                }

            }
            TempData["Msg"] = "Record Inserted Error";
            return View("ScreenList");
        }
        #endregion

        #region Add Screen
        public IActionResult ScreenAdd(int ScreenID)
        {
            ScreenModel screenModel = screenDAL.PR_Screens_SelectByID(ScreenID);

            if (screenModel.ScreenID != null)
            {
                TempData["PageTitle"] = "Screen Edit Page";
                ViewBag.CinemaList = screenDAL.PR_Cinemas_ComboBox();
                ViewBag.MovieList = screenDAL.PR_Movies_ComboBox();
                return View("ScreenAdd", screenModel);
            }
            else
            {
                TempData["PageTitle"] = "Screen Edit Page";
                ViewBag.CinemaList = screenDAL.PR_Cinemas_ComboBox();
                ViewBag.MovieList = screenDAL.PR_Movies_ComboBox();
                return View("ScreenAdd");
            }
        }
        #endregion

        #region Delete Screen
        public IActionResult ScreenDelete(int ScreenID)
        {
            bool iSuccess = screenDAL.PR_Screens_Delete(ScreenID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("ScreenList");

            }

            return RedirectToAction("ScreenList");
        }
        #endregion

        #region Screen Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    ScreenDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("ScreenList");
        }
        #endregion

        #region GetScreenModel
        public List<ScreenModel> GetScreenModel()
        {
            List<ScreenModel> models = new List<ScreenModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Screens_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ScreenModel model = new ScreenModel
                    {
                        ScreenID = Convert.ToInt32(reader["ScreenID"]),
                        ScreenName = reader["ScreenName"].ToString(),
                        Capacity = Convert.ToInt32(reader["Capacity"]),
                        CinemaName = reader["CinemaName"].ToString(),
                        Title = reader["Title"].ToString()
                        // Add other properties as needed
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

            List<ScreenModel> models = GetScreenModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Screens");
                // Add headers
                worksheet.Cell(1, 1).Value = "ScreenID";
                worksheet.Cell(1, 2).Value = "Screenanme";
                worksheet.Cell(1, 3).Value = "Capacity";
                worksheet.Cell(1, 4).Value = "CinemaName";
                worksheet.Cell(1, 5).Value = "Title";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.ScreenID;
                    worksheet.Cell(row, 2).Value = model.ScreenName;
                    worksheet.Cell(row, 3).Value = model.Capacity;
                    worksheet.Cell(row, 4).Value = model.CinemaName;
                    worksheet.Cell(row, 5).Value = model.Title;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "Screens.xlsx";
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
