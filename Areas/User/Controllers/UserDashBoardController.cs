using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.Areas.User.Model;
using BookMovieShow.BAL;
using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.CinemaWithMovies;
using BookMovieShow.DAL.MovieDetail;
using BookMovieShow.DAL.MovieList;
using BookMovieShow.DAL.MST_Movie;
using BookMovieShow.DAL.UserDashBoard;
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
        CinemasDAL cinemasDAL = new CinemasDAL(); 
        CinemaWithMoviesDAL cWMDAL = new CinemaWithMoviesDAL();
        MovieListDAL Ml = new MovieListDAL();
        UserDashboard_DAL UD  = new UserDashboard_DAL();
        MovieDetail_DALBase movieDetail_DAL = new MovieDetail_DAL();

        #region User_DashBoard
        public IActionResult User_DashBoard(int StateID, int CityID)
        {
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
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
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            ViewBag.CityList = UD.PR_City_ComboBoxbyStateID(StateID);
            ViewBag.CinemaList = UD.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
            DataTable dt = new DataTable();
             dt = UD.PR_Movies_SelectAll();
            return View(dt);
        }
        #endregion

        //#region MovieListByCity
        //public IActionResult MovieListByCity(int StateID, int CityID, int CinemaID)
        //{
        //    ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
        //    ViewBag.CityList = UD.PR_City_ComboBoxbyStateID(StateID);
        //    ViewBag.CinemaList = UD.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
        //    DataTable dt = new DataTable();
        //    dt = Ml.PR_Movies_SelectByStateCityAndCinemaID(StateID, CityID, CinemaID);
        //    return View("User_DashBoard", dt);
        //}
        //#endregion

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
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            return View("User_DashBoard", dt); 
        }
        #endregion

    }
}
