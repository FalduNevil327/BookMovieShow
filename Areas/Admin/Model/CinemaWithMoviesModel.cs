using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class CinemaWithMoviesModel
    {
        public int ID { get; set; }
        [Required]
        public int? MovieID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public int? CinemaID { get; set; }
        [Required]
        public string? CinemaName { get; set; }


    }

    public class CinemaWithMovies_FilterModel
    {
        public int? ID { get; set; }
        public string? Title { get; set; }
        public string? CinemaName { get; set; }
    }
}
