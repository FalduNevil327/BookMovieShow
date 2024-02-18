using Microsoft.AspNetCore.Mvc;
using System.Data;
using BookMovieShow.DAL.LOC_City;
using System.Data.SqlClient;
using BookMovieShow.Areas.Admin.Model;
using ClosedXML.Excel;

namespace BookMovieShow.Areas.LOC_City.Controllers
{

    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class LOC_CityController : Controller
    {
        LOC_CityDAL cityDAL = new LOC_CityDAL();

        private IConfiguration Configuration;
        public LOC_CityController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region SelectAll city
        public IActionResult LOC_CityList()
        {
            ViewBag.StateList = cityDAL.PR_State_ComboBox();
            ViewBag.CityList = cityDAL.PR_City_ComboBox();
            DataTable dt = cityDAL.PR_City_SelectAll();
            return View(dt);
        }
        #endregion

        #region Save City
        public IActionResult LOC_CitySave(LOC_CityModel modelCity)
        {
            if (ModelState.IsValid)
            {
                if (cityDAL.PR_City_Insert(modelCity))
                {
                    return RedirectToAction("LOC_CityList");
                }

            }
            return View("LOC_CityAdd");
        }
        #endregion

        #region Add City
        public IActionResult LOC_CityAdd(int CityID)
        {
            LOC_CityModel modelCity = cityDAL.PR_City_SelectByPK(CityID);

            if (modelCity != null)
            {
                TempData["PageTitle"] = "Country Edit Page";
                ViewBag.StateList = cityDAL.PR_State_ComboBox();
                return View("LOC_CityAdd", modelCity);
            }
            else
            {
                TempData["PageTitle"] = "Country Add Page";
                ViewBag.StateList = cityDAL.PR_State_ComboBox();
                return View("LOC_CityAdd");
            }
        }
        #endregion

        #region Delete City
        public IActionResult LOC_CityDelete(int CityID)
        {
            bool iSuccess = cityDAL.PR_City_Delete(CityID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("LOC_CityList");

            }

            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region City Filter
        public IActionResult LOC_CityFilter(LOC_CityFilterModel filterModel)
        {
            DataTable dt = cityDAL.PR_City_Filter(filterModel);
            ViewBag.StateList = cityDAL.PR_State_ComboBox();
            return View("LOC_CityList", dt);
        }
        #endregion

        #region City DropDown
        public IActionResult CityDropDownByStateID(int CityID)
        {
            string myconnStr1 = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection1 = new SqlConnection(myconnStr1);
            DataTable dt1 = new DataTable();
            connection1.Open();
            SqlCommand cmd1 = connection1.CreateCommand();
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.CommandText = "PR_City_ComboBoxbyStateId";
            cmd1.Parameters.AddWithValue("@CityID", CityID);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            dt1.Load(reader1);

            List<LOC_CityDropDownModel> list = new List<LOC_CityDropDownModel>();

            foreach (DataRow dr in dt1.Rows)
            {
                LOC_CityDropDownModel lstList = new LOC_CityDropDownModel();
                lstList.CityID = Convert.ToInt32(dr["CityID"]);
                lstList.CityName = dr["CityName"].ToString();
                list.Add(lstList);
            }
            var vModel = list;
            return Json(vModel);

        }
        #endregion

        #region City Multiple Delete
        [HttpPost]
        public ActionResult LOC_CityMultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    LOC_CityDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("LOC_CityList");
        }
        #endregion

        #region GetCityModel
        public List<LOC_CityModel> GetCityModel()
        {
            List<LOC_CityModel> models = new List<LOC_CityModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_City_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    LOC_CityModel model = new LOC_CityModel
                    {
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                        StateName = reader["StateName"].ToString(),

                        // Add other properties as needed
                    };
                    models.Add(model);
                }
                return models;
            }
        }

        #endregion

        #region City_ExportSToExcel
        public IActionResult City_ExportSToExcel()
        {

            List<LOC_CityModel> models = GetCityModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LOC_City");
                // Add headers
                worksheet.Cell(1, 1).Value = "CityName";
                worksheet.Cell(1, 2).Value = "CityCode";
                worksheet.Cell(1, 3).Value = "StateName";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.CityName;
                    worksheet.Cell(row, 2).Value = model.CityCode;
                    worksheet.Cell(row, 3).Value = model.StateName;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "LOC_City.xlsx";
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

