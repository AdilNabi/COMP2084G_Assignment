using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084G_Assignment.Models
{
    public class Book
    {
        //  pk field
        public int BookId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(100)]
        public string Author { get; set; }

        public byte[] Image { get; set; }


        //  child reference
        public List<Rating> Ratings { get; set; }

        //  parent reference
        public Genre Genre { get; set; }


    }
}
