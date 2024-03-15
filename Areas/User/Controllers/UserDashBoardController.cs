using BookMovieShow.BAL;
using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.CinemaWithMovies;
using BookMovieShow.DAL.MovieList;
using BookMovieShow.DAL.UserDashBoard;
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
        
        public IActionResult User_DashBoard()
        {
            ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
            ViewBag.CinemaList = cWMDAL.PR_Cinemas_ComboBox();
            DataTable dt = new DataTable();
            dt = UD.GetRecentMovies();
            return View(dt);
        }
        
    }
}
