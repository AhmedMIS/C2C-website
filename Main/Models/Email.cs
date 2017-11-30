using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sql4.Models
{
    public class Email
    {



        [Required(ErrorMessage = "You forgot to enter a username.")]
        [Display(Name = "Name")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 12 characters.")]
        string name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
  
        string s_email { get; set; }
        [Required]
        string mess { get; set; }
        int rid { get; set; }
        string r_email { get; set; }
         


        





    }
}