using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class MST_CinemaModel
    {
        public int CinemaID { get; set; } 

        [Required]
        public string? CinemaName { get; set; }


        public int? StateID { get; set; }
        public string? StateName { get; set; } = string.Empty;
        public int? CityID { get; set; }
        public string? CityName { get; set; } = string.Empty;

        [Required]
        public int? Capacity { get; set; }

        [Required]
        public int? ScreenNumber { get; set; }
    }

    public class MST_CinemaFilterModel
    {
        public int? CinemaID { get; set; }
        public string? CinemaName { get; set; }
        public int? StateID { get; set; }
        public int? CityID { get; set; }
    }

    public class MST_CinemaDropDownModel
    {
        public int? CinemaID { get; set; }
        public string? CinemaName { get; set; }
    }
}
