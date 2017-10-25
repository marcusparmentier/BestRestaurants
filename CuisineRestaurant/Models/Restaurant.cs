using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CuisineRestaurant.Models
{
  public class Restaurant
  {
    private string _name;
    private int _cuisineId;
    private int _id;
    private string _hours;
    private string _dish;

    public Restaurant(string name, int cuisineId, string hours, string dish, int id = 0)
    {
      _name = name;
      _cuisineId = cuisineId;
      _id = id;
      _hours = hours;
      _dish = dish;
    }

    public override bool Equals(System.Object otherRestaurant)
    {
      if (!(otherRestaurant is Restaurant))
      {
        return false;
      }
      else
      {
        Restaurant newRestaurant = (Restaurant) otherRestaurant;
        bool idEquality = this.GetId() == newRestaurant.GetId();
        bool nameEquality = this.GetName() == newRestaurant.GetName();
        bool cuisineEquality = this.GetCuisineId() == newRestaurant.GetCuisineId();
        bool hoursEquality = this.GetHours() == newRestaurant.GetHours();
        bool dishEquality = this.GetDish() == newRestaurant.GetDish();
        return (idEquality && nameEquality && cuisineEquality && hoursEquality && dishEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetCuisineId()
    {
      return _cuisineId;
    }

    public string GetHours()
    {
      return _hours;
    }

    public string GetDish()
    {
      return _dish;
    }



    public static List<Restaurant> GetAll()
    {
      List<Restaurant> allRestaurant = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string restaurantName = rdr.GetString(0);
        int id = rdr.GetInt32(1);
        int cuisineId = rdr.GetInt32(2);
        string hours = rdr.GetString(3);
        string dish = rdr.GetString(4);
        Restaurant newRestaurant = new Restaurant(restaurantName, cuisineId, hours, dish, id);
        allRestaurant.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurant;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO restaurant (name, cuisineId, hours, dish) VALUES (@name, @cuisineId, @hours, @dish);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      MySqlParameter cuisineId = new MySqlParameter();
      cuisineId.ParameterName = "@cuisineId";
      cuisineId.Value = this._cuisineId;
      cmd.Parameters.Add(cuisineId);

      MySqlParameter hours = new MySqlParameter();
      hours.ParameterName = "@hours";
      hours.Value = this._hours;
      cmd.Parameters.Add(hours);

      MySqlParameter dish = new MySqlParameter();
      dish.ParameterName = "@dish";
      dish.Value = this._dish;
      cmd.Parameters.Add(dish);


      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }

    public static Restaurant Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int restaurantId = 0;
      string restaurantName = "";
      int restaurantCuisineId = 0;
      string restaurantHours = "";
      string restaurantDish = "";

      while(rdr.Read())
      {
        restaurantName = rdr.GetString(0);
        restaurantId = rdr.GetInt32(1);
        restaurantCuisineId = rdr.GetInt32(2);
        restaurantHours = rdr.GetString(3);
        restaurantDish = rdr.GetString(4);
      }
      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantHours, restaurantDish, restaurantId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public static Restaurant SearchDish(string dish)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant WHERE dish = (@searchDish);";

      MySqlParameter searchDish = new MySqlParameter();
      searchDish.ParameterName = "@searchDish";
      searchDish.Value = dish;
      cmd.Parameters.Add(searchDish);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int restaurantId = 0;
      string restaurantName = "";
      int restaurantCuisineId = 0;
      string restaurantHours = "";
      string restaurantDish = "";

      while(rdr.Read())
      {
        restaurantName = rdr.GetString(0);
        restaurantId = rdr.GetInt32(1);
        restaurantCuisineId = rdr.GetInt32(2);
        restaurantHours = rdr.GetString(3);
        restaurantDish = rdr.GetString(4);
      }
      Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantHours, restaurantDish, restaurantId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newRestaurant;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM restaurant;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
