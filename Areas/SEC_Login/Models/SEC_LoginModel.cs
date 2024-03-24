namespace BookMovieShow.Areas.SEC_Login.Models
{
    public class SEC_LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public int PhoneNumber { get; set; }
        public DateTime? RegistrationDate {  get; set; }
        public string? Address {  get; set; }
        public string? ProfileImage {  get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsActive { get; set; }
    }

    


}
