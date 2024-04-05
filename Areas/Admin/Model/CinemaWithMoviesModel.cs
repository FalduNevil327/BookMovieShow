using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class CinemaWithMoviesModel
    {
        
        public int? ID { get; set; }

        [Required(ErrorMessage = "Please select at least one movie.")]
        public List<int>? MovieIDs { get; set; }

        public string? Title { get; set; }

        [Required(ErrorMessage = "Please enter the cinema ID.")]
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
