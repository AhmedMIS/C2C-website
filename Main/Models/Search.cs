using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sql4.Models
{
    public class Search
    {
        [Required]
        [Display(Name = "Title")]
        [StringLength(50, MinimumLength = 12, ErrorMessage = "Username must be between 12 and 50 characters.")]
        string search { get; set; }
        [Required]
        [Display(Name = "Catagory")]
        string cat { get; set; }
        [Required]
          [Display(Name = "City")]
        string city { get; set; }

    }
}