using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using CuisineRestaurant.Models;

namespace CuisineRestaurant.Test
{
  [TestClass]
  public class RestaurantTests
  {
    public RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=cuisine_restaurant_test;";
    }

    [TestMethod]
    public void GetAll_RestaurantEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Restaurant.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverRideTrueForSameDescription_Restaurant()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Wendy's", 1);
      Restaurant secondRestaurant = new Restaurant("Wendy's", 1);

      //Assert
      Assert.AreEqual(firstRestaurant, secondRestaurant);
    }
  }
}
