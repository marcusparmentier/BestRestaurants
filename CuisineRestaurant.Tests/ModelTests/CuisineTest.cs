using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using CuisineRestaurant.Models;

namespace CuisineRestaurant.Test
{
    [TestClass]
    public class CuisineTests : IDisposable
    {
        public CuisineTests()
        {
          DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=cuisine_restaurant_test;";
        }

        public void Dispose()
        {
          Cuisine.DeleteAll();
        }

        [TestMethod]
        public void GetAll_CuisinesEmptyAtFirst_0()
        {
            //Arrange, Act
            int result = Cuisine.GetAll().Count;

            //Assert
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void Equals_ReturnsTrueForSameName_Cuisine()
        {
          //Arrange, Act
          Cuisine firstCuisine = new Cuisine("Orange Chicken");
          Cuisine secondCuisine = new Cuisine("Orange Chicken");

          //Assert
          Assert.AreEqual(firstCuisine, secondCuisine);
        }
    }
}
