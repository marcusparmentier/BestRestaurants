using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using CuisineRestaurant.Models;

namespace CuisineRestaurant.Test
{
  [TestClass]
  public class RestaurantTests : IDisposable
  {
    public RestaurantTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=cuisine_restaurant_test;";
    }
    public void Dispose()
    {
      Restaurant.DeleteAll();

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
    public void Equals_OverRideTrueForSameName_Restaurant()
    {
      //Arrange, Act
      Restaurant firstRestaurant = new Restaurant("Wendy's", 1, "8am - 10pm", "Cheeseburger");
      Restaurant secondRestaurant = new Restaurant("Wendy's", 1, "8am - 10pm", "Cheeseburger");

      //Assert
      Assert.AreEqual(firstRestaurant, secondRestaurant);
    }

    [TestMethod]
    public void Save_SavesRestaurantToDatabase_RestaurantList()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("KFC", 100, "8am - 10pm", "Chicken");
      testRestaurant.Save();

      //Act
      // string resultString = "";
      // int resultId = 0;
      // int Cuisine = 0;
      List<Restaurant> result = Restaurant.GetAll();
      // foreach(var list in result)
      // {
      //   resultString = list.GetName();
      //   resultId = list.GetId();
      //   Cuisine = list.GetCuisineId();
      // }
      // Console.WriteLine(resultString + " " + resultId + " " + Cuisine);
      List<Restaurant> testList = new List<Restaurant>{testRestaurant};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("KFC", 1, "8am - 10pm", "Chicken");
      testRestaurant.Save();

      //Act
      Restaurant savedRestaurant = Restaurant.GetAll()[0];

      int result = savedRestaurant.GetId();
      int testId = testRestaurant.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsRestaurantInDatabase_Restaurant()
    {
      //Arrange
      Restaurant testRestaurant = new Restaurant("KFC", 1, "8am - 10pm", "Chicken");
      testRestaurant.Save();

      //Act
      Restaurant foundRestaurant = Restaurant.Find(testRestaurant.GetId());

      //Assert
      Assert.AreEqual(testRestaurant, foundRestaurant);
    }

  }
}
