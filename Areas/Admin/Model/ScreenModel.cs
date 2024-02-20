namespace BookMovieShow.Areas.Admin.Model
{
    public class ScreenModel
    {
        public int? ScreenID {  get; set; }
        public string? ScreenName { get; set; }
        public int? Capacity { get; set; }
        public int? CinemaID {  get; set; }
        public String? CinemaName { get; set; } = string.Empty;
        public int? MovieID {  get; set; }
        public string? Title { get; set; } = string.Empty;
        
    }

    public class Screens_DropDownModel
    {
        public int? ScreenID { get; set; }
        public string? ScreenName { get; set; }
    }
}

