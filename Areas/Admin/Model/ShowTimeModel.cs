using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class ShowTimeModel
    {
        public int? ShowTimeID { get; set; }
        [Required]
        public int? MovieID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int? CinemaID { get; set; }
        [Required]
        public string? CinemaName { get; set; }
        [Required]
        public int? ScreenID { get; set; }
        [Required]
        public String? ScreenName {  get; set; }
        [Required]
        public int? AvailableSeats { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public DateTime? ShowTime { get; set; }
    }
}
