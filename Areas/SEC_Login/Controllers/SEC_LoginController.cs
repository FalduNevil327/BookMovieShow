using BookMovieShow.Areas.SEC_Login.Models;
using BookMovieShow.DAL.SEC_Login;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMovieShow.Areas.SEC_Login.Controllers
{
    [Area("SEC_Login")]
    [Route("SEC_Login/[controller]/[action]")]
    public class SEC_LoginController : Controller
    {

        SEC_LoginDAL loginDal = new SEC_LoginDAL();

        #region Configuration
        private readonly IConfiguration Configuration;

        public SEC_LoginController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }
        #endregion

        public IActionResult SEC_LoginPage()
        {
            return View();
        }

        #region Login
        [HttpPost]
        public IActionResult Login(SEC_LoginModel modelSEC_User)
        {
            string error = null;
            //Console.WriteLine("Hello ", modelSEC_User.Name);
            if (modelSEC_User.UserName == null)
            {
                error += "User Name is required";
            }
            if (modelSEC_User.Password == null)
            {
                error += "<br/>Password is required";
            }

            if (error != null)
            {
                TempData["Error"] = error;
                return RedirectToAction("SEC_UserLogin");
            }
            else
            {
                SEC_LoginDAL sEC_UserDAL = new SEC_LoginDAL();
                DataTable dt = sEC_UserDAL.PR_SEC_Login_SelectByUserNamePassword(modelSEC_User.UserName, modelSEC_User.Password);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                        HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                        HttpContext.Session.SetString("Password", dr["Password"].ToString());
                        HttpContext.Session.SetString("Email", dr["Email"].ToString());
                        HttpContext.Session.SetString("FullName", dr["FullName"].ToString());
                        //HttpContext.Session.SetString("CityName", dr["CityName"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        HttpContext.Session.SetString("PhoneNumber", dr["PhoneNumber"].ToString());
                        HttpContext.Session.SetString("IsAdmin", dr["IsAdmin"].ToString());
                        HttpContext.Session.SetString("IsActive", dr["IsActive"].ToString());

                        break;
                    }
                }
                else
                {
                    TempData["Error"] = "User Name or Password is invalid!";
                    return RedirectToAction("SEC_LoginPage", "Login", new { area = "SEC_Login" });
                }
                if (HttpContext.Session.GetString("UserName") != null && HttpContext.Session.GetString("Password") != null)
                {
                    if (HttpContext.Session.GetString("IsAdmin") == "True")
                    {
                        return RedirectToAction("Admin_DashBoard", "Admin", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("User_DashBoard", "User", new { area = "User" });
                    }
                }
            }
            return RedirectToAction("SEC_LoginPage", "SEC_Login", new { area = "SEC_Login" });
        }

        #endregion

        

    }
}
