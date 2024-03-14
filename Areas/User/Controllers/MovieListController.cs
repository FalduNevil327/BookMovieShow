using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.CinemaWithMovies;
using BookMovieShow.DAL.MovieList;
using DocumentFormat.OpenXml.Bibliography;
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

        
        public IActionResult User_MovieList()
        {
            ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            ViewBag.CinemaList = cWMDAL.PR_Cinemas_ComboBox();
            DataTable dt = new DataTable();
            dt = Ml.PR_Movies_SelectAll();
            return View(dt);
        }

        public IActionResult MovieListByCity(int CityID)
        {
            ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            ViewBag.CinemaList = cWMDAL.PR_Cinemas_ComboBox();
            DataTable dt = new DataTable();
            dt = Ml.PR_Movies_SelectByID(CityID);
            return View("User_MovieList", dt);
        }

    }
}
