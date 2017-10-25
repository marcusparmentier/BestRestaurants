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
  }
}
