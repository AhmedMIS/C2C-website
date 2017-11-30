using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace sql4.Models
{
    public class send
    {
        [Required(ErrorMessage = "You forgot to enter a Subject.")]
        [Display(Name = "Subject")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Username must be between 6 and 12 characters.")]
        public string title { get; set; }


       
        [Required(ErrorMessage = "Must full the message body")]
        [Display(Name = "MESSAGE")]
        [DataType(DataType.MultilineText)]
        [StringLength(2000, MinimumLength = 50, ErrorMessage = "Username must be between 10 and 200 characters.")]
        public string description { get; set; }

        [Required(ErrorMessage = "You forgot to enter a Name.")]
        [Display(Name = "Name")]
        [StringLength(12, MinimumLength = 6, ErrorMessage = "Username must be between 3 and 12 characters.")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Your Email")]
  
        public string e_mail { get; set; }





        [RegularExpression(@"[0-9]*\.?[0-9]+", ErrorMessage = "Price must be a Numbers only.")]
        
        public decimal phone { get; set; }
    }
}