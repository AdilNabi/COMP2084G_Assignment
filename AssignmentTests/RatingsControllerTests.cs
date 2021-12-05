using COMP2084G_Assignment.Controllers;
using COMP2084G_Assignment.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using COMP2084G_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AssignmentTests
{
    [TestClass]
    public class RatingsControllerTests
    {
        // class level vars for db
        private ApplicationDbContext _context;
        private RatingsController controller;
        List<Rating> ratings = new List<Rating>();

        [TestInitialize]
        public void TestInitialize()
        {
            // set up in-memory db
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new ApplicationDbContext(options);

            // create mock data for the testing
            var Genre = new Genre 
            { 
                GenreId = 1000,
                Name = "Test Genre" 
            };

            var Book = new Book
            {
                BookId = 123,
                Title = "Test Book",
                Author = "Test Person",
                GenreId = 1000,
                Genre = Genre,
                Image = "Test Image"
            };

            ratings.Add(new Rating
            {
                RatingId = 201,
                Heading = "Test Heading",
                Name = "Test Reviewer",
                Body = "Test Body",
                Score = 7,
                BookId = 123,
                Book = Book
            });

            ratings.Add(new Rating
            {
                RatingId = 301,
                Heading = "The Heading",
                Name = "The Reviewer",
                Body = "The Body",
                Score = 8,
                BookId = 123,
                Book = Book
            });

            ratings.Add(new Rating
            {
                RatingId = 401,
                Heading = "Amazing Heading",
                Name = "Amazing Reviewer",
                Body = "Amazing Body",
                Score = 4,
                BookId = 123,
                Book = Book
            });

            foreach (var rating in ratings)
            {
                _context.Ratings.Add(rating);
            }
            _context.SaveChanges();

            controller = new RatingsController(_context);

        }

        #region Index
        [TestMethod]
         public void IndexLoadsCorrectView()
         {

            // act
            var result = (ViewResult)controller.Index().Result;

            // assert
            Assert.AreEqual("Index", result.ViewName);
         }

        [TestMethod]
        public void IndexLoadsRatings()
        {
            // act
            var result = (ViewResult)controller.Index().Result;
            List<Rating> model = (List<Rating>)result.Model;

            // assert

            CollectionAssert.AreEqual(ratings.OrderBy(b => b.Heading).ToList(), model);
        }

        #endregion

        #region Details
        [TestMethod]
        public void DetailsNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);

        }

        [TestMethod]
        public void DetailsInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Details(-1).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);

        }

        [TestMethod]
        public void DetailsIsValidIdLoadsRating()
        {
            // act
            var result = (ViewResult)controller.Details(201).Result;
            Rating rating = (Rating)result.Model;

            // assert
            Assert.AreEqual(ratings[0], rating);
        }

        [TestMethod]
        public void DetailsValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Details(201).Result;

            // assert
            Assert.AreEqual("Details", result.ViewName);
        }
        #endregion



    }

}
