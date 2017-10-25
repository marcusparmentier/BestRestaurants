using System;
using MySql.Data.MySqlClient;
using CuisineRestaurant;

namespace CuisineRestaurant.Models
{
  public class DB
  {
    public static MySqlConnection Connection()
    {
      MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
      return conn;
    }
  }
}
