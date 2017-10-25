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

    public Restaurant(string name, int cuisineId, int id = 0)
    {
      _name = name;
      _cuisineId = cuisineId;
      _id = id;
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
       return (idEquality && nameEquality && cuisineEquality);
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
        Restaurant newRestaurant = new Restaurant(restaurantName, id, cuisineId);
        allRestaurant.Add(newRestaurant);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allRestaurant;
    }



    // public void Save()
    // {
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"INSERT INTO restaurants (name, cuisineId) VALUES (@name, @cuisineId);";
    //
    //   MySqlParameter name = new MySqlParameter();
    //   name.ParameterName = "@name";
    //   name.Value = this._name;
    //   cmd.Parameters.Add(name);
    //
    //   MySqlParameter cuisineId = new MySqlParameter();
    //   cuisineId.ParameterName = "@cuisineId";
    //   cuisineId.Value = this._cuisineId;
    //   cmd.Parameters.Add(cuisineId);
    //
    //
    //   cmd.ExecuteNonQuery();
    //   _id = (int) cmd.LastInsertedId;
    //   conn.Close();
    //   if (conn != null)
    //   {
    //     conn.Dispose();
    //   }
    //
    // }
  }
}
