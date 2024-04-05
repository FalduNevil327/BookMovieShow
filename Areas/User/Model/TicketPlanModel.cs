namespace BookMovieShow.Areas.User.Model
{
    public class TicketPlanModel
    {
        public string? CinemaName { get; set; }
        public DateTime? ShowTime { get; set; }
        public int? ShowTimeID { get; set; }
        public int MovieID { get; set; }
        public string? Title {  get; set; }
        public string? Genre {  get; set; }
    }
}
