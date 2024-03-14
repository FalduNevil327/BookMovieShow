using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.User.Model
{
    public class MovieDetailModel
    {
        public int MovieID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Duration { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        public string? Language { get; set; }
        [Required]
        public string? Director { get; set; }
        [Required]
        public string? Genre { get; set; }
        [Required]
        public Decimal? Rating { get; set; }


        public string? PosterImageURL { get; set; }

        public string? TrailerURL { get; set; }
    }
}
