using BookMovieShow.Areas.Admin.Model;
using BookMovieShow.DAL.Cinemas;
using BookMovieShow.DAL.Profile;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BookMovieShow.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class ProfileController : Controller
    {
        Profile_DAL profile_DAL = new Profile_DAL();


        private IConfiguration Configuration;

        public ProfileController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }

        #region SelectAll
        public IActionResult Profile(int UserID)
        {
            if (TempData["UserID"] != null)
            {
                UserID = Convert.ToInt32(TempData["UserID"]);
            }
            ProfileModel profileModel = new ProfileModel();
            profileModel = profile_DAL.PR_Profile_SelectByID(UserID);
            return View(profileModel);
        }
        #endregion

        #region Profile Save
        public IActionResult Profile_Save(ProfileModel profileModel)
        {
            if (ModelState.IsValid)
            {
                if (profile_DAL.PR_Profile_Update(profileModel))
                {
                    TempData["Msg"] = "Record Updated Successfully";
                    TempData["UserID"] = profileModel.UserID;
                    return RedirectToAction("Profile");
                }

            }
            TempData["Msg"] = "Record Inserted Error";
            return View("Profile");
        }
        #endregion

        #region Update Profile
        public IActionResult ProfileUpdate(int UserID)
        {
            ProfileModel profileModel = profile_DAL.PR_Profile_SelectByID(UserID);

            if (profileModel != null)
            {
                TempData["PageTitle"] = "Profile Edit Page";
                //ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
                //ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
                return View("ProfileUpdate", profileModel);
            }
            else
            {
                TempData["PageTitle"] = "Profile Edit Page";
                //ViewBag.StateList = cinemasDAL.PR_State_ComboBox();
                //ViewBag.CityList = cinemasDAL.PR_City_ComboBox();
                return View("ProfileUpdate");
            }
        }
        #endregion
    }
}
