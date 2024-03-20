using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.Customer;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class MST_UserController : Controller
    {
        MST_UserDAL userDAL = new MST_UserDAL();

        private IConfiguration Configuration;

        public MST_UserController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult MST_UserList()
        {
            //ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            //ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            DataTable dt = userDAL.PR_User_SelectAll();
            return View(dt);
        }
        #endregion

        #region User Save
        public IActionResult MST_UserSave(MST_UserModel mST_UserModel)
        {
            if (ModelState.IsValid)
            {
                if (userDAL.PR_User_Insert(mST_UserModel))
                {
                    TempData["Msg"] = "Record Inserted Successfully";
                    return RedirectToAction("MST_UserList");
                }

            }
            TempData["Msg"] = "Record Inserted Error";
            return View("MST_UserList");
        }
        #endregion

        #region Add User
        public IActionResult MST_UserAddEdit(int UserID)
        {
            MST_UserModel userModel = userDAL.PR_User_SelectByID(UserID);

            if (userModel != null)
            {
                TempData["PageTitle"] = "User Edit Page";
                //ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
                //ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
                return View("MST_UserAddEdit", userModel);
            }
            else
            {
                TempData["PageTitle"] = "User Edit Page";
                //ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
                //ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
                return View("MST_UserAddEdit");
            }
        }
        #endregion

        #region Delete User
        public IActionResult MST_UserDelete(int UserID)
        {
            bool iSuccess = userDAL.PR_User_Delete(UserID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("MST_UserList");

            }

            return RedirectToAction("MST_UserList");
        }
        #endregion

        #region User Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    MST_UserDelete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("MST_UserList");
        }
        #endregion

        #region GetUserModel
        public List<MST_UserModel> GetUserModel()
        {
            List<MST_UserModel> models = new List<MST_UserModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_User_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    MST_UserModel model = new MST_UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Password = reader["Password"].ToString(),
                        FullName = reader["FullName"].ToString(),
                        Email = reader["Email"].ToString(),
                        PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]),
                        Address = reader["Address"].ToString(),
                        //RegistrationDate = Convert.ToDateTime(reader["RegistrationDate"]),
                        //IsActive = Convert.ToBoolean(reader["IsActive"]),
                        //IsAdmin = Convert.ToBoolean(reader["IsAdmin"]),
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

            List<MST_UserModel> models = GetUserModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("MST_User");
                // Add headers
                worksheet.Cell(1, 1).Value = "UserID";
                worksheet.Cell(1, 2).Value = "UserName";
                worksheet.Cell(1, 3).Value = "Password";
                worksheet.Cell(1, 4).Value = "FullName";
                worksheet.Cell(1, 5).Value = "Email";
                worksheet.Cell(1, 6).Value = "PhoneNumber";
                worksheet.Cell(1, 7).Value = "Address";
                //worksheet.Cell(1, 8).Value = "RegistrationDate";
                //worksheet.Cell(1, 9).Value = "IsActive";
                //worksheet.Cell(1, 10).Value = "IsAdmin";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.UserID;
                    worksheet.Cell(row, 2).Value = model.UserName;
                    worksheet.Cell(row, 3).Value = model.Password;
                    worksheet.Cell(row, 4).Value = model.FullName;
                    worksheet.Cell(row, 5).Value = model.Email;
                    worksheet.Cell(row, 6).Value = model.PhoneNumber;
                    worksheet.Cell(row, 7).Value = model.Address;
                    //worksheet.Cell(row, 8).Value = model.RegistrationDate;
                    //worksheet.Cell(row, 9).Value = (XLCellValue)model.IsActive;
                    //worksheet.Cell(row, 10).Value = (XLCellValue)model.IsAdmin;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "MST_User.xlsx";
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
