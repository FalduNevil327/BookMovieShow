﻿using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class LOC_CityModel
    {
        public int? CityID { get; set; }
        [Required]
        public string? CityName { get; set; }
        [Required]
        public string? CityCode { get; set; }
        [Required]
        public int? StateID { get; set; }
        [Required]
        public string? StateName { get; set; }

    }

    public class LOC_CityDropDownModel
    {
        public int CityID { get; set; }
        public string? CityName { get; set; }

    }

    public class LOC_CityFilterModel
    {
        public int? StateID { get; set; }
        public string? CityName { get; set; }
        public string? CityCode { get; set; }
    }
}
