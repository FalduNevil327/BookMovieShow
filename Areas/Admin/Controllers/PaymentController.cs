using BookMovieShow.Areas.Admin.Model;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using BookMovieShow.DAL.Admin.Payment;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class PaymentController : Controller
    {
        Payment_DAL payment_DAL = new Payment_DAL();

        private IConfiguration Configuration;
        public PaymentController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult Payment_List()
        {
            DataTable dt = payment_DAL.PR_Payment_SelectAll();
            return View(dt);
        }
        #endregion

        #region Payment Save
        public IActionResult Payment_Save(PaymentModel paymentModel)
        {
            if (payment_DAL.PR_Payment_Insert(paymentModel))
            {
                TempData["Msg"] = "Record Inserted Successfully";
                return RedirectToAction("Payment_List");
            }

            TempData["Msg"] = "Record Inserted Error";
            return View("Payment_List");
        }
        #endregion

        #region Add Payment
        public IActionResult Payment_Add(int PaymentID)
        {
            PaymentModel paymentModel = payment_DAL.PR_Payment_SelectByID(PaymentID);

            if (paymentModel.PaymentID != null)
            {
                TempData["PageTitle"] = "Payment Edit Page";
                return View("Payment_Add", paymentModel);
            }
            else
            {
                TempData["PageTitle"] = "Payment Edit Page";
                return View("Payment_Add");
            }
        }
        #endregion

        #region Delete Payment
        public IActionResult Payment_Delete(int PaymentID)
        {
            bool iSuccess = payment_DAL.PR_Payment_Delete(PaymentID);

            if (iSuccess)
            {
                TempData["Msg"] = "Record Deleted Successfully";
                return RedirectToAction("Payment_List");

            }

            return RedirectToAction("Payment_List");
        }
        #endregion

        #region Payment_Search
        public IActionResult Payment_Search(string? UserName)
        {

            DataTable dt = payment_DAL.PR_Payment_SearchByUserName(UserName);
            return View("Payment_List", dt);
        }
        #endregion

        #region Payment Multiple Delete
        [HttpPost]
        public ActionResult MultipleDelete(int[] id)
        {

            foreach (var item in id)
            {
                try
                {
                    Payment_Delete(item);
                    Console.WriteLine("Deleted " + item);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return RedirectToAction("Payment_List");
        }
        #endregion

        #region GetPaymentModel
        public List<PaymentModel> GetPaymentModel()
        {
            List<PaymentModel> models = new List<PaymentModel>();
            string myconnStr = this.Configuration.GetConnectionString("ConnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Payment_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PaymentModel model = new PaymentModel
                    {
                        PaymentID = Convert.ToInt32(reader["PaymentID"]),
                        UserID = Convert.ToInt32(reader["UserID"]),
                        ShowTimeID = Convert.ToInt32(reader["ShowTimeID"]),
                        Amount = Convert.ToInt32(reader["Amount"]),
                        PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                        PaymentStatus = reader["PaymentStatus"].ToString()
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

            List<PaymentModel> models = GetPaymentModel();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Payment");
                // Add headers
                worksheet.Cell(1, 1).Value = "PaymentID";
                worksheet.Cell(1, 2).Value = "UserID";
                worksheet.Cell(1, 3).Value = "ShowTimeID";
                worksheet.Cell(1, 4).Value = "Amount";
                worksheet.Cell(1, 5).Value = "PaymentDate";
                worksheet.Cell(1, 6).Value = "PaymnetStatus";
                // Add data
                int row = 2;
                foreach (var model in models)
                {
                    worksheet.Cell(row, 1).Value = model.PaymentID;
                    worksheet.Cell(row, 2).Value = model.UserID;
                    worksheet.Cell(row, 3).Value = model.ShowTimeID;
                    worksheet.Cell(row, 4).Value = model.Amount;
                    worksheet.Cell(row, 5).Value = model.PaymentDate;
                    worksheet.Cell(row, 6).Value = model.PaymentStatus;
                    // Add other properties...
                    row++;
                }
                // Set content type and filename
                var contentType = "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet";
                var fileName = "Payment.xlsx";
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
