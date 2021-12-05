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

        #region Create
        [TestMethod]
        public void CreateLoadsValidList()
        {
           // act
            var result = controller.Create();

            // assert
            var resultViewData = controller.ViewData["BookId"];

            Assert.IsNotNull(result);          
        }

        [TestMethod]
        public void CreateLoadsCorrectView()
        {
            // act
            var result = (ViewResult)controller.Create();

            // assert
            Assert.AreEqual("Create", result.ViewName);
        }


        [TestMethod]
        public void CreateSaveToDb()
        {
            var rating = new Rating
            {
                RatingId = 605,
                Heading = "Test Heading",
                Name = "Test Reviewer",
                Body = "Test Body",
                Book = new Book { BookId = 987, Title = "Test Book", Author = "John Doe", Image = "Test Image", Genre = new Genre { GenreId = 456, Name = "Test Genre" } }
            };

            _context.Ratings.Add(rating);
            _context.SaveChanges();

            Assert.AreEqual(rating, _context.Ratings.Find(605));
        }
        #endregion

        #region Edit
        [TestMethod]
        public void EditNoIdLoads404()
        {
            var result = (ViewResult)controller.Edit(null).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditInvalidIdLoads404()
        {
            var result = (ViewResult)controller.Edit(-1).Result;

            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void EditIsValidIdLoadView()
        {
            var result = (ViewResult)controller.Edit(201).Result;
            Assert.AreEqual("Edit", result.ViewName);
        }

        [TestMethod]
        public void EditLoadsValidModel()
        {
            var result = (ViewResult)controller.Edit(201).Result;

            Assert.AreEqual(_context.Ratings.Find(201), (Rating)result.Model);

        }
        #endregion




        #region Delete
        [TestMethod]
        public void DeleteNoIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(null).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);

        }

        [TestMethod]
        public void DeleteInvalidIdLoads404()
        {
            // act
            var result = (ViewResult)controller.Delete(-1).Result;

            // assert
            Assert.AreEqual("404", result.ViewName);
        }

        [TestMethod]
        public void DeleteValidIdLoadsView()
        {
            // act
            var result = (ViewResult)controller.Delete(201).Result;

            // assert
            Assert.AreEqual("Delete", result.ViewName);
        }


        [TestMethod]
        public void DeleteIsValidIdLoadsRating()
        {
            // act
            var result = (ViewResult)controller.Delete(201).Result;
            Rating rating = (Rating)result.Model;

            // assert
            Assert.AreEqual(ratings[0], rating);
        }

        [TestMethod]
        public void DeleteValidIdConfirm()
        {
            var result = controller.DeleteConfirmed(201);

            var rating = _context.Ratings.Find(201);

            Assert.AreEqual(rating, null);
        }


        [TestMethod]
        public void DeleteValidIdConfirmRedirect()
        {
            var result = controller.DeleteConfirmed(201);

            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);

        }
        #endregion


    }

}
