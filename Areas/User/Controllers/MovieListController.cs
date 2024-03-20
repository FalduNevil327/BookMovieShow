using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.CinemaWithMovies;
using BookMovieShow.DAL.MovieList;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class MovieListController : Controller
    {
        CinemasDAL cinemasDAL = new CinemasDAL();
        CinemaWithMoviesDAL cWMDAL = new CinemaWithMoviesDAL();
        MovieListDAL Ml = new MovieListDAL();

        
        public IActionResult User_MovieList(int StateID, int CityID)
        {
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            ViewBag.CityList = Ml.PR_City_ComboBoxbyStateID(StateID);
            ViewBag.CinemaList = Ml.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
            DataTable dt = new DataTable();
            dt = Ml.PR_Movies_SelectAll();
            return View(dt);
        }

        public IActionResult MovieListByCity(int StateID, int CityID, int CinemaID)
        {
            ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
            ViewBag.CityList = Ml.PR_City_ComboBoxbyStateID(StateID);
            ViewBag.CinemaList = Ml.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
            DataTable dt = new DataTable();
            dt = Ml.PR_Movies_SelectByStateCityAndCinemaID(StateID,CityID,CinemaID);
            return View("User_MovieList", dt);
        }
        #region CityDropDownByStateID
        public IActionResult CityDropDownByStateID(int StateID)
        {
            var model = Ml.PR_City_ComboBoxbyStateID(StateID);
            return Json(model);

        }
        #endregion

        #region CinemaDropDownByStateIDAndCityID

        public IActionResult CinemaDropDownByStateIDAndCityID(int StateID, int CityID)
        {
            var model = Ml.PR_Cinema_ComboBoxByStateIDAndCityID(StateID, CityID);
            return Json(model);

        }
        #endregion

    }
}
