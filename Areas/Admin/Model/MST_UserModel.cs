using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class MST_UserModel
    {
        public int? UserID{ get; set; }

        [Required(ErrorMessage = "Please Enter Your Full Name Here")]
        public string? FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter Your UserName Here")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Please Enter Your PassWord Here")]
        public string? Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please Enter Your Phonenumber Here")]
        public int? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email Here")]
        public string? Email { get; set; }
        public string? Address { get; set; }
        //public DateTime? RegistrationDate { get; set; }

        //public bool? IsActive { get; set; }
        //public bool? IsAdmin { get; set; }

    }
}
