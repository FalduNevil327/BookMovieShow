namespace BookMovieShow.Areas.Admin.Model
{
    public class BookingModel
    {
        public int? BookingID { get; set; } 
        public int? UserID { get; set; } 
        public int? ShowTimeID { get; set; } 
        public int? NumberOfTickets { get; set; }   
        public string? SeatNumbers { get; set; }
        public decimal? TotalAmount { get; set; }
        public DateTime? BookingDate { get; set; }
        public string? BookingStatus { get; set;}
    }
}
