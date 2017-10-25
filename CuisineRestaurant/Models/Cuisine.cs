using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace CuisineRestaurant.Models
{
  public class Cuisine
  {
    private string _name;
    private int _id;

    public Cuisine(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public string GetName()
    {
      return _name;
    }

    public int GetId()
    {
      return _id;
    }

    public override bool Equals(System.Object otherCuisine)
    {
      if(!(otherCuisine is Cuisine))
      {
        return false;
      }
      else
      {
        Cuisine newCuisine = (Cuisine) otherCuisine;
        return this.GetId().Equals(newCuisine.GetId());
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO cuisine (name) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._name;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

    }

    public static List<Cuisine> GetAll()
    {
      List<Cuisine> allCuisine = new List<Cuisine> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string cuisineName = rdr.GetString(0);
        int cuisineId = rdr.GetInt32(1);
        Cuisine newCuisine = new Cuisine(cuisineName, cuisineId);
        allCuisine.Add(newCuisine);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCuisine;
    }

    public static Cuisine Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cuisine WHERE id = (@searchId);";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = id;
      cmd.Parameters.Add(searchId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CuisineId = 0;
      string CuisineName = "";

      while(rdr.Read())
      {
        CuisineName = rdr.GetString(0);
        CuisineId = rdr.GetInt32(1);
      }
      Cuisine newCuisine = new Cuisine(CuisineName, CuisineId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newCuisine;
    }

    public List<Restaurant> GetRestaurant()
    {
      List<Restaurant> allCuisineRestaurant = new List<Restaurant> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM restaurant WHERE cuisineId = @cuisineId;";

      MySqlParameter CuisineId = new MySqlParameter();
      CuisineId.ParameterName = "@cuisineId";
      CuisineId.Value = this._id;
      cmd.Parameters.Add(CuisineId);


      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        string restaurantName = rdr.GetString(0);
        int restaurantId = rdr.GetInt32(1);
        int restaurantCuisineId = rdr.GetInt32(2);
        string restaurantHours = rdr.GetString(3);
        string restaurantDish = rdr.GetString(4);
        Restaurant newRestaurant = new Restaurant(restaurantName, restaurantCuisineId, restaurantHours, restaurantDish, restaurantId);
        allCuisineRestaurant.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allCuisineRestaurant;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cuisine;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
