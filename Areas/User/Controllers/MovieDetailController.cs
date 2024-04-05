using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.User.MovieDetail;
using Microsoft.AspNetCore.Mvc;

namespace BookMovieShow.Areas.User.Controllers
{
    [Area("User")]
    [Route("User/[controller]/[action]")]
    public class MovieDetailController : Controller
    {
        MovieDetail_DAL movieDetail_DAL = new MovieDetail_DAL();
       
        #region Movie_Detail
        public IActionResult MovieDetail(int MovieID)
        {
            MST_MovieModel mST_MovieModel = movieDetail_DAL.PR_Movies_SelectByID(MovieID);
            return View("MovieDetail", mST_MovieModel);

        }
        #endregion
    }
}
