namespace BookMovieShow.Areas.User.Model
{
    public class UserDashBoard_FilterModel
    {
        //public int? CinemaID { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
        public string? Title { get; set; }
    }

    public class MovieList_FilterModel
    {
        public string? Language { get; set; }
        public string? Genre { get; set; }
    }
}
