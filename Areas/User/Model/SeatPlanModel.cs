namespace BookMovieShow.Areas.User.Model
{
    public class SeatPlanModel
    {
        public int? MovieID { get; set; }
        public int? Price { get; set; }
        public string? Title { get; set; }
        public string? Genre { get; set; }
        public string? Language { get; set; }
        public string? CinemaName { get; set; }
        public DateTime? ShowTime { get; set; }
    }
}
