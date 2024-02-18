using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.CinemaWithMovies;
using BookMovieShow.DAL.MST_Movie;
using ClosedXML.Excel;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Data;
using System.Data.SqlClient;


namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class CinemaWithMoviesController : Controller
    {
        CinemaWithMoviesDAL cWMDAL = new CinemaWithMoviesDAL();

        private IEnumerable<object> models;

        private readonly IConfiguration _configuration;

        public CinemaWithMoviesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region SelectAll
        public IActionResult CinemaWithMovies_List()
        {
            ViewBag.CinemaList = cWMDAL.PR_Cinemas_ComboBox();
            ViewBag.MovieList = cWMDAL.PR_Movies_ComboBox();
            DataTable dt = cWMDAL.PR_CinemaWithMovies_SelectAll();
            return View(dt);
        }
        #endregion

        #region CinemaWithMovies Save
        public IActionResult CinemaWithMovies_Save(CinemaWithMoviesModel cWM)
        {
            if (ModelState.IsValid)
            {
                if (cWMDAL.PR_CinemaWithMovies_Insert(cWM))

                {
                    Console.WriteLine(cWM.MovieID);
                    TempData["Msg"] = "Record Inserted Successfully";
                    return RedirectToAction("CinemaWithMovies_List");
                }

            }
            TempData["Msg"] = "Record Updated Successfully";
            return View("CinemaWithMovies_Add");
        }
        #endregion

        #region Add CinemaWithMovies
        public IActionResult CinemaWithMovies_Add(int ID)
        {
            CinemaWithMoviesModel cWMModel = cWMDAL.PR_CinemaWithMovies_SelectByID(ID);

            if (cWMModel != null)
            {
                ViewBag.CinemaList = cWMDAL.PR_Cinemas_ComboBox();
                ViewBag.MovieList = cWMDAL.PR_Movies_ComboBox();
                return View("CinemaWithMovies_Add", cWMModel);
            }
            else
            {
                return View("CinemaWithMovies_Add");
            }
        }
        #endregion

        #region Delete CinemaWithMovies
        public IActionResult CinemaWithMovies_Delete(int ID)
        {
            bool iSuccess = cWMDAL.PR_CinemaWithMovies_Delete(ID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("CinemaWithMovies_List");

            }
            TempData["Msg"] = "Record Deleted Successfully";
            return RedirectToAction("CinemaWithMovies_List");
        }
        #endregion

        #region Filter
        public IActionResult CinemaWithMovies_Filter(CinemaWithMovies_FilterModel filterModel)
        {
            ViewBag.CinemaList = cWMDAL.PR_Cinemas_ComboBox();
            ViewBag.MovieList = cWMDAL.PR_Movies_ComboBox();
            DataTable dt = cWMDAL.PR_CinemasWithMovies_Filter(filterModel);
            return View("CinemaWithMovies_List", dt);
        }
        #endregion

        #region CinemaWthMovies Multiple Delete
        [HttpPost]
        public ActionResult CinemaWithMovies_MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    CinemaWithMovies_Delete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("CinemaWithMovies_List");
        }
        #endregion

        #region GetCinemaWithMoviesModel
        public List<CinemaWithMoviesModel> GetCinemaWithMoviesModel()
        {
            List<CinemaWithMoviesModel> models = new List<CinemaWithMoviesModel>();
            string myconnStr = this._configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_CinemaWithMovies_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    CinemaWithMoviesModel model = new CinemaWithMoviesModel
                    {
                        ID = Convert.ToInt32(reader["ID"]),
                        CinemaName = reader["CinemaName"].ToString(),
                        Title = reader["Title"].ToString(),

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

            List<CinemaWithMoviesModel> models = GetCinemaWithMoviesModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("CinemaWithMovies");
                // Add headers
                worksheet.Cell(1, 1).Value = "ID";
                worksheet.Cell(1, 2).Value = "CinemaName";
                worksheet.Cell(1, 3).Value = "Title";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.ID;
                    worksheet.Cell(row, 2).Value = model.CinemaName;
                    worksheet.Cell(row, 3).Value = model.Title;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "CinemaWithMovies.xlsx";
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
