using System.ComponentModel.DataAnnotations;

namespace BookMovieShow.Areas.Admin.Model
{
    public class LOC_StateModel
    {   
        public int? StateID { get; set;}
        
        public string? StateName { get; set;}
       
        public string? StateCode { get; set; }
        
    }

    public class LOC_StateDropDownModel
    {
        public int StateID { get; set; }
        public string? StateName { get; set; }

    }

    public class LOC_StateFilterModel
    {
        public string? StateName { get; set;}
        public string? StateCode { get; set;}
    }
}
