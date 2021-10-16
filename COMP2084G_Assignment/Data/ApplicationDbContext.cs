using COMP2084G_Assignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace COMP2084G_Assignment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        //  declaring a DbSet object for each of the models
        public DbSet<Genre> Genres { get; set; }
        
        public DbSet<Book> Books { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
