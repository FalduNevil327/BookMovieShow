using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class MST_MovieModel
    {
        public int MovieID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Language { get; set; }
       public string? Director { get; set; }
        public string? Genre { get; set; }
        public Decimal? Rating { get; set; }
        
        public string? PosterImageURL { get; set; }
       
        public string? TrailerURL { get; set;}

    }

    public class MST_MovieFilterModel
    {
        public int? MovieID { get; set; }
        public string? Genre { get; set; }
        public string? Language { get; set; }
        public decimal? Rating { get; set; }
    }

    public class MST_MoviesDropDownModel
    {
        public int? MovieID { get; set; }
        public string? Title { get; set; }
    }

}
