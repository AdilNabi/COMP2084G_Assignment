using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2084G_Assignment.Models
{
    public class Genre
    {
        //  pk field
        public int GenreId { get; set; }

        //  Making input field mandatory
        [Required(AllowEmptyStrings = false, ErrorMessage = "Genre Name required.")]
        public string Name { get; set; }

        //  child reference
        public List<Book> Books { get; set; }
    }
}
