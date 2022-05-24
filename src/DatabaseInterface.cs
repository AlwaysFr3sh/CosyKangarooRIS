using System;
using System.Data.SQLite;
using CosyKangaroo.Models;

namespace CosyKangaroo.Database {
  // Method to get database version (for testing)
  public static class DatabaseInterface {

    public static string ConnectionString = "DataSource=mydatabase.db;Version=3;New=True;Compress=True;";
    public static SQLiteConnection sqlite_conn;

    public static void OpenDatabaseConnection() {
      sqlite_conn = new SQLiteConnection(ConnectionString);
      try {
        sqlite_conn.Open();
      } 
      catch (Exception ex) {
        Console.WriteLine($"Error connecting to database: {ex.Message}"); 
      }
    }
    public static void CloseDatabaseConnection() {
      sqlite_conn.Close(); 
    }

    // Returns database version (for testing purposes)
    public static string DatabaseVersion() {
      string stm = "SELECT SQLITE_VERSION()";
      using var cmd = new SQLiteCommand(stm, sqlite_conn);
      string version = cmd.ExecuteScalar().ToString();
      return version;
    }

    // an assumption I wrote in Assignment2 said we aren't doing encryption so I won't worry about that here - Tom
    // Currently we are parsing 4 strings instead of Person object, it would be cleaner if we parsed Person object both ways
    // but I'm not sure what to do about null id's for new Person's going into the database
    public static void RegisterUser(Person user, string password) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "INSERT INTO person (username, password, phone, address) VALUES ($username, $password, $phone, $address);";
      sqlite_cmd.Parameters.AddWithValue("$username", user.GetName());
      sqlite_cmd.Parameters.AddWithValue("$password", password);
      sqlite_cmd.Parameters.AddWithValue("$phone", user.GetPhone());
      sqlite_cmd.Parameters.AddWithValue("$address", user.GetAddress());
      sqlite_cmd.ExecuteNonQuery();
    }
  }
}
