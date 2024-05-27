namespace BookMovieShow.Areas.User.Model
{
    public class SeatSelectionModel
    {
        public int SeatSelectionID { get; set; }
        public List<string>? SelectedSeats { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
