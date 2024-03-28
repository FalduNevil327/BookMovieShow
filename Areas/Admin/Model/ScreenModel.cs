using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class ScreenModel
    {
        public int? ScreenID {  get; set; }
        [Required]
        public string? ScreenName { get; set; }
        [Required]
        public int? Capacity { get; set; }
        [Required]
        public int? CinemaID {  get; set; }
        [Required]
        public String? CinemaName { get; set; } = string.Empty;
        [Required]
        public int? MovieID {  get; set; }
        [Required]
        public string? Title { get; set; } = string.Empty;
            
        
    }

    public class Screens_DropDownModel
    {
        public int? ScreenID { get; set; }
        public string? ScreenName { get; set; }
    }

    public class Screen_FilterModel
    {
        public int? ID { get; set; }
        public string? Title { get; set; }
        public string? CinemaName { get; set; }
        public string? ScreenName { get; set; }
    }
}

