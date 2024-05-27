namespace BookMovieShow.Areas.User.Model
{
    public class BookingDetailModel
    {
        public int ShowtimeID { get; set; }
        public int MovieID { get; set; }
        public int CinemaID { get; set; }
        public int SeatCount { get; set; }
        public List<string> SeatNumbers { get; set; }
        public string Title { get; set; }
        public string CinemaName { get; set; }
        public string Language { get; set; }
        public string Genre { get; set; }
        public DateTime Showtime { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal price { get; set; }
        public int NumberOfTickets {  get; set; }
        public DateTime BookingDate { get; set; }


    }
}
