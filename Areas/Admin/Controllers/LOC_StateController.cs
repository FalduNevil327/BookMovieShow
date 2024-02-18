using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using BookMovieShow.DAL.LOC_State;
using BookMovieShow.Areas.Admin.Model;
using ClosedXML.Excel;

namespace BookMovieShow.Areas.LOC_State.Controllers
{

    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class LOC_StateController : Controller
    {
        LOC_StateDAL stateDAL = new LOC_StateDAL();

        private IConfiguration Configuration;
        public LOC_StateController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }


        public IActionResult Index()
        {
            return View();
        }

        #region SelectAll State
        public IActionResult LOC_StateList()
        {
            ViewBag.StateList = stateDAL.PR_State_ComboBox();
            DataTable dt = stateDAL.PR_State_SelectAll();
            return View(dt);
        }
        #endregion

        #region Add state
        public IActionResult LOC_StateAdd(int StateID)
        {

            LOC_StateModel stateModel = stateDAL.PR_State_SelectByPK(StateID);

            if (stateModel != null)
            {
                ViewBag.StateList = stateDAL.PR_State_ComboBox();
                return View("LOC_StateAddEdit", stateModel);
            }
            else
            {
                return View("LOC_StateAddEdit");
            }
        }
        #endregion

        #region Save State
        [HttpPost]
        public IActionResult LOC_StateSave(LOC_StateModel stateModel)
        {
            if (ModelState.IsValid)
            {
                if (stateDAL.PR_State_Insert(stateModel))
                {
                    return RedirectToAction("LOC_StateList");
                }

            }
            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region Delete State
        public IActionResult LOC_StateDelete(int StateID)
        {
            bool iSuccess = stateDAL.PR_State_Delete(StateID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("LOC_StateList");

            }

            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region State Filter
        public IActionResult LOC_StateFilter(LOC_StateFilterModel filterModel)
        {
            DataTable dt = stateDAL.PR_State_Filter(filterModel);
            
            return View("LOC_StateList", dt);
        }
        #endregion

        #region State Multiple Delete
        [HttpPost]
        public ActionResult LOC_StateMultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    LOC_StateDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("LOC_StateList");
        }
        #endregion

        #region GetStateModel
        public List<LOC_StateModel> GetStateModel()
        {
            List<LOC_StateModel> models = new List<LOC_StateModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_State_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    LOC_StateModel model = new LOC_StateModel
                    {
                        StateName = reader["StateNAme"].ToString(),
                        StateCode = reader["StateCode"].ToString(),

                        // Add other properties as needed
                    };
                    models.Add(model);
                }
                return models;
            }
        }

        #endregion

        #region State_ExportSToExcel
        public IActionResult State_ExportSToExcel()
        {

            List<LOC_StateModel> models = GetStateModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("LOC_State");
                // Add headers
                worksheet.Cell(1, 1).Value = "StateName";
                worksheet.Cell(1, 2).Value = "StateCode";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.StateName;
                    worksheet.Cell(row, 2).Value = model.StateCode;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "LOC_State.xlsx";
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

