using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084G_Assignment.Models
{
    public class Rating
    {
        //  pk field
        public int RatingId { get; set; }

        [Display(Name = "Book")]
        //  fk field
        public int BookId { get; set; }

        [Required]
        [MaxLength(150)]
        [Display(Name = "Reviewer's Name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Heading { get; set; }

        [Required]
        [Range(1,10)]
        public int Score { get; set; }

        //  below will create a textbox for the body
        [Required]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }


        // parent reference
        public Book Book { get; set; }
        

    }
}
