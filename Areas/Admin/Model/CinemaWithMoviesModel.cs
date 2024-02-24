namespace BookMovieShow.Areas.Admin.Model
{
    public class CinemaWithMoviesModel
    {
        public int ID { get; set; }
        public int? MovieID { get; set; }
        public string? Title { get; set; }
        public int? CinemaID { get; set; }
        public string? CinemaName { get; set; }


    }

    public class CinemaWithMovies_FilterModel
    {
        public int? ID { get; set; }
        public string? Title { get; set; }
        public string? CinemaName { get; set; }
    }
}
