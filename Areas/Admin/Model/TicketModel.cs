namespace BookMovieShow.Areas.Admin.Model
{
    public class TicketModel
    {
        public int? TicketID { get; set; }
        public int? BookingID { get; set; }
        public int? ShowTimeID { get; set; }
        public int? UserID { get; set; }
        public string? SeatNumbers { get; set; }
        public string? TicketStatus { get; set; }
        public string? QRCode { get; set; }
        public string? TicketMode { get; set; }

    }
}
