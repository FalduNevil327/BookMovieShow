using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.User.Model;
using BookMovieShow.BAL;
using BookMovieShow.DAL.Admin.Cinemas;
using BookMovieShow.DAL.Admin.CinemaWithMovies;
using BookMovieShow.DAL.User.MovieDetail;
using BookMovieShow.DAL.User.MovieList;
using BookMovieShow.DAL.User.UserDashBoard;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMovieShow.Areas.User.Controllers
{

    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class UserDashBoardController : Controller
    {
        
        UserDashboard_DAL UD  = new UserDashboard_DAL();

        #region User_DashBoard
        public IActionResult User_DashBoard(int StateID, int CityID)
        {
            ViewBag.StateList = UD.PR_State_ComboBox();
            ViewBag.CityList = UD.PR_City_ComboBoxbyStateID(StateID);
            ViewBag.CinemaList = UD.PR_Cinema_ComboBoxByStateIDAndCityID(StateID,CityID);
            DataTable dt = new DataTable();
            dt = UD.GetRecentMovies();
            return View(dt);
        }
        #endregion

        #region MovieList
        public IActionResult MovieList(int StateID, int CityID)
        {
            ViewBag.StateList = UD.PR_State_ComboBox();
            ViewBag.CityList = UD.PR_City_ComboBoxbyStateID(StateID);
            ViewBag.CinemaList = UD.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
            DataTable dt = new DataTable();
             dt = UD.PR_Movies_SelectAll();
            return View(dt);
        }
        #endregion

        #region CityDropDownByStateID
        public IActionResult CityDropDownByStateID(int StateID)
        {
            var model = UD.PR_City_ComboBoxbyStateID(StateID);
            return Json(model);

        }
        #endregion

        #region CinemaDropDownByStateIDAndCityID

        public IActionResult CinemaDropDownByStateIDAndCityID(int StateID, int CityID)
        {
            var model = UD.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
            return Json(model);

        }
        #endregion

        #region Movie Filter
        public IActionResult UserDashBoard_Filter(UserDashBoard_FilterModel filterModel)
        {
            DataTable dt = UD.PR_UserDashBoard_Filter(filterModel);
            ViewBag.StateList = UD.PR_State_ComboBox();
            ViewBag.CityList = UD.PR_City_ComboBox();
            return View("User_DashBoard", dt); 
        }
        #endregion

    }
}
