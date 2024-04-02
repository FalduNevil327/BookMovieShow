using BookMovieShow.Areas.Admin.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using BookMovieShow.DAL.MST_Movie;
using ClosedXML.Excel;
using System.Data.SqlClient;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using BookMovieShow.DAL.Cinemas;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CinemaController : Controller
    {
        CinemasDAL cinemasDAL = new CinemasDAL();


        private IConfiguration Configuration;

        public CinemaController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult MST_CinemaList()
        {
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            DataTable dt = cinemasDAL.PR_Cinemas_SelectAll();
            return View(dt);
        }
        #endregion

        #region Cinema Save
        public IActionResult MST_CinemaSave(MST_CinemaModel mST_CinemaModel)
        {
            if (ModelState.IsValid)
            {
                if (cinemasDAL.PR_Cinemas_Insert(mST_CinemaModel))
                {
                    TempData["Msg"] = "Record Inserted Successfully";
                    return RedirectToAction("MST_CinemaList");
                }

            }
            TempData["Msg"] = "Record Inserted Error";
            return View("MST_CinemaList");
        }
        #endregion

        #region Add Cinema
        public IActionResult MST_CinemaAdd(int CinemaID)
        {
            MST_CinemaModel cinemaModel = cinemasDAL.PR_Cinemas_SelectByID(CinemaID);

            if (cinemaModel != null)
            {
                TempData["PageTitle"] = "Cinema Edit Page";
                ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
                ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
                return View("MST_CinemaAdd", cinemaModel);
            }
            else
            {
                TempData["PageTitle"] = "Cinema Edit Page";
                ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
                ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
                return View("MST_CinemaAdd");
            }
        }
        #endregion

        #region Delete Cinema
        public IActionResult MST_CinemaDelete(int CinemaID)
        {
            bool iSuccess = cinemasDAL.PR_Cinemas_Delete(CinemaID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("MST_CinemaList");

            }

            return RedirectToAction("MST_CinemaList");
        }
        #endregion

        #region CityDropDownByStateID
        public IActionResult CityDropDownByStateID(int StateID)
        {
            var model = cinemasDAL.PR_City_ComboBoxbyStateID(StateID);
            return Json(model);

        }
        #endregion

        #region Cinema Detail
        public IActionResult MST_CinemaDetails(int CinemaID = 0)
        {
            MST_CinemaModel mST_CinemaModel = cinemasDAL.PR_Cinemas_SelectByID(CinemaID);
            return View("MST_CinemaDetails", mST_CinemaModel);

        }
        #endregion

        #region Cinema Filter
        public IActionResult MST_CinemaFilter(MST_CinemaFilterModel filterModel)
        {
            DataTable dt = cinemasDAL.PR_Cinemas_Filter(filterModel);
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            return View("MST_CinemaList", dt);
        }
        #endregion

        #region Cinema Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    MST_CinemaDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("MST_CinemaList");
        }
        #endregion

        #region GetCinemaModel
        public List<MST_CinemaModel> GetCinemaModel()
        {
            List<MST_CinemaModel> models = new List<MST_CinemaModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Cinemas_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine("Hello");
                    MST_CinemaModel model = new MST_CinemaModel
                    {
                        
                        CinemaID = Convert.ToInt32(reader["CinemaID"]),
                        CinemaName = reader["CinemaName"].ToString(),
                        Capacity = Convert.ToInt32(reader["Capacity"]),
                        ScreenNumber = Convert.ToInt32(reader["ScreenNumber"]),
                        StateName = reader["StateName"].ToString(),
                        CityName = reader["CityName"].ToString(),
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

            List<MST_CinemaModel> models = GetCinemaModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MST_Cinema");
                // Add headers
                worksheet.Cell(1, 1).Value = "CinemaID";
                worksheet.Cell(1, 2).Value = "CinemaName";
                worksheet.Cell(1, 3).Value = "Capacity";
                worksheet.Cell(1, 4).Value = "ScreenNumber";
                worksheet.Cell(1, 5).Value = "StateName";
                worksheet.Cell(1, 6).Value = "CityName";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.CinemaID;
                    worksheet.Cell(row, 2).Value = model.CinemaName;
                    worksheet.Cell(row, 3).Value = model.Capacity;
                    worksheet.Cell(row, 4).Value = model.ScreenNumber;
                    worksheet.Cell(row, 5).Value = model.StateName;
                    worksheet.Cell(row, 6).Value = model.CityName;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "MST_Cinema.xlsx";
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
