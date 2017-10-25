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
      Restaurant.DeleteAll();
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

    [TestMethod]
    public void Save_SavesCuisineToDatabase_CuisineList()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Orange Chicken");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }


    [TestMethod]
    public void Save_DatabaseAssignsIdToCuisine_Id()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Orange Chicken");
      testCuisine.Save();

      //Act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.GetId();
      int testId = testCuisine.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsCuisineInDatabase_Cuisine()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Orange Chicken");
      testCuisine.Save();

      //Act
      Cuisine foundCuisine = Cuisine.Find(testCuisine.GetId());

      //Assert
      Assert.AreEqual(testCuisine, foundCuisine);
    }

    [TestMethod]
    public void GetRestaurants_RetrievesAllRestaurantsWithCuisine_RestaurantList()
    {
      Cuisine testCuisine = new Cuisine("Household chores");
      testCuisine.Save();

      Restaurant firstRestaurant = new Restaurant("Mow the lawn", testCuisine.GetId());
      firstRestaurant.Save();
      Restaurant secondRestaurant = new Restaurant("Do the dishes", testCuisine.GetId());
      secondRestaurant.Save();


      List<Restaurant> testRestaurantList = new List<Restaurant> {firstRestaurant, secondRestaurant};
      List<Restaurant> resultRestaurantList = testCuisine.GetRestaurant();

      CollectionAssert.AreEqual(testRestaurantList, resultRestaurantList);
    }
  }
}
