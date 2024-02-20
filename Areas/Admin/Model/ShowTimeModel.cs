namespace BookMovieShow.Areas.Admin.Model
{
    public class ShowTimeModel
    {
        public int? ShowTimeID { get; set; }
        public int? MovieID { get; set; }
        public string? Title { get; set; }
        public int? CinemaID { get; set; }
        public string? CinemaName { get; set; }
        public int ScreenID { get; set; }
        public String? ScreenName {  get; set; }
        public int? AvailableSeats { get; set; }
        public decimal? Price { get; set; }
        public TimeOnly? ShowTime { get; set; }
    }
}
