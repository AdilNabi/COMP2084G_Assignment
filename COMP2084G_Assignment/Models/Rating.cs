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

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [Range(1,10)]
        public int Score { get; set; }


        // parent reference
        public Book Book { get; set; }
        

    }
}
