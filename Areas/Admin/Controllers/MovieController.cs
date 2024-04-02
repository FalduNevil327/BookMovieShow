using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.MST_Movie;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;
using System.Data.SqlClient;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MovieController : Controller
    {
        MST_MovieDAL mST_MovieDAL = new MST_MovieDAL();

        public IActionResult MST_MovieDetails()
        {
            return View();
        }
        private IConfiguration Configuration;
        public MovieController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult MST_MovieList()
        {
            DataTable dt = mST_MovieDAL.PR_Movies_SelectAll();
            return View(dt);
        }
        #endregion

        #region Movie Save
        public IActionResult MST_MovieSave(MST_MovieModel mST_MovieModel)
        {
            if (ModelState.IsValid)
            {
                if (mST_MovieDAL.PR_Movies_Insert(mST_MovieModel))
                {
                    TempData["Msg"] = "Record Inserted Successfully";
                    return RedirectToAction("MST_MovieList");
                }

            }
            return View("MST_MovieAdd");
        }
        #endregion

        #region Add Movie
        public IActionResult MST_MovieAdd(int MovieID)
        {
            MST_MovieModel movieModel = mST_MovieDAL.PR_Movies_SelectByID(MovieID);

            if (movieModel != null)
            {

                return View("MST_MovieAdd", movieModel);
            }
            else
            {
                return View("MST_MovieAdd");
            }
        }
        #endregion

        #region Delete Movie
        public IActionResult MST_MovieDelete(int MovieID)
        {
            bool iSuccess = mST_MovieDAL.PR_Movies_Delete(MovieID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("MST_MovieList");

            }

            return RedirectToAction("MST_MovieList");
        }
        #endregion

        #region Movie_Detail
        public IActionResult MST_MovieDetail(int MovieID = 0)
        {
            MST_MovieModel mST_MovieModel = mST_MovieDAL.PR_Movies_SelectByID(MovieID);
            return View("MST_MovieDetail", mST_MovieModel);

        }
        #endregion

        #region Movie Filter
        public IActionResult MST_MovieFilter(MST_MovieFilterModel filterModel)
        {
            DataTable dt = mST_MovieDAL.PR_Movies_Filter(filterModel);
            return View("MST_MovieList", dt);
        }
        #endregion

        #region Movie Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    MST_MovieDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("MST_MovieList");
        }
        #endregion

        #region GetMovieModel
        public List<MST_MovieModel> GetMovieModel()
        {
            List<MST_MovieModel> models = new List<MST_MovieModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Movies_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    MST_MovieModel model = new MST_MovieModel
                    {

                        MovieID = Convert.ToInt32(reader["MovieID"]),
                        Title = reader["Title"].ToString(),
                        Description = reader["Description"].ToString(),
                        Genre = reader["Genre"].ToString(),
                        Language = reader["Language"].ToString(),
                        Duration = reader["Duration"].ToString(),
                        ReleaseDate = Convert.ToDateTime(reader["ReleaseDate"]),
                        Director = reader["Director"].ToString(),
                        Rating = Convert.ToDecimal(reader["Rating"]),
                        PosterImageURL = reader["PosterImageURL"].ToString(),
                        TrailerURL = reader["TrailerURL"].ToString(),
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

            List<MST_MovieModel> models = GetMovieModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MST_Movie");
                // Add headers
                worksheet.Cell(1, 1).Value = "MovieID";
                worksheet.Cell(1, 2).Value = "Title";
                worksheet.Cell(1, 3).Value = "Description";
                worksheet.Cell(1, 4).Value = "Genre";
                worksheet.Cell(1, 5).Value = "Language";
                worksheet.Cell(1, 6).Value = "ReleaseDate";
                worksheet.Cell(1, 7).Value = "Director";
                worksheet.Cell(1, 8).Value = "Rating";
                worksheet.Cell(1, 9).Value = "PosterImageURL";
                worksheet.Cell(1, 10).Value = "TrailerURL";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.MovieID;
                    worksheet.Cell(row, 2).Value = model.Title;
                    worksheet.Cell(row, 3).Value = model.Description;
                    worksheet.Cell(row, 4).Value = model.Genre;
                    worksheet.Cell(row, 5).Value = model.Language;
                    worksheet.Cell(row, 6).Value = model.ReleaseDate;
                    worksheet.Cell(row, 7).Value = model.Director;
                    worksheet.Cell(row, 8).Value = model.Rating;
                    worksheet.Cell(row, 9).Value = model.PosterImageURL;
                    worksheet.Cell(row, 10).Value = model.TrailerURL;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "MST_Movie.xlsx";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

        #endregion

        #region CityDropDownByStateID
        public IActionResult CityDropDownByStateID(int StateID)
        {
            var model = mST_MovieDAL.PR_City_ComboBoxbyStateID(StateID);
            return Json(model);

        }
        #endregion



    }
}
