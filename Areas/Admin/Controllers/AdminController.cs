using BookMovieShow.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class AdminController : Controller
    {

        private readonly IConfiguration Configuration;

        public AdminController(IConfiguration _Configuration)
        {
            Configuration = _Configuration;
        }

        public IActionResult Admin_DashBoard()
        {
            string connectionStr = this.Configuration.GetConnectionString("ConnectionString");
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(connectionStr);
            connection.Open();
            SqlCommand objCmd = connection.CreateCommand();
            objCmd.CommandType = CommandType.StoredProcedure;
            objCmd.CommandText = "[PR_AdminDashboard_Counts]";
            SqlDataReader objSDR = objCmd.ExecuteReader();
            dt.Load(objSDR);
            Console.WriteLine(dt.Rows.Count);
            return View(dt);
        }
    }
}
