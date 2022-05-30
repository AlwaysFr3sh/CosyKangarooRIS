using System;
using System.Data.SQLite;
using CosyKangaroo.Models;

namespace CosyKangaroo.Database {
  // Method to get database version (for testing)
  public static class DatabaseInterface {

    public static string ConnectionString = "DataSource=mydatabase.db;Version=3;New=True;Compress=True;";
    public static SQLiteConnection sqlite_conn = new SQLiteConnection(ConnectionString);

    public static void OpenDatabaseConnection() {
      //sqlite_conn = new SQLiteConnection(ConnectionString);
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
      using var sqlite_cmd = new SQLiteCommand(stm, sqlite_conn);
      //string version = cmd.ExecuteScalar().ToString();
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      string version = rdr.GetString(0);
      rdr.Close();

      return version;
    }

    // an assumption I wrote in Assignment2 said we aren't doing encryption so I won't worry about that here - Tom
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

    // check if user exists provided a username
    public static bool UserExists(string username) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT username FROM person WHERE username = @username";
      sqlite_cmd.Parameters.AddWithValue("@username", username);
      //string ret = sqlite_cmd.ExecuteScalar().ToString(); 
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      string ret = rdr.GetString(0);
      rdr.Close();

      return ret == username; 
    }

    // Check if password is correct
    public static bool AuthenticatePassword(string username, string password) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT password FROM person WHERE username = @username";
      sqlite_cmd.Parameters.AddWithValue("@username", username);
      //string ret = sqlite_cmd.ExecuteScalar().ToString();
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      string ret = rdr.GetString(0);
      rdr.Close();

      return ret == password;
    }

    // For now we assume that the user actually exists
    // Maybe later we do some exception handling or something or maybe not idk.
    // Also...
    // TODO: Currently we are using Person class
    // At some point we should edit the schema so we can determine if we return Customer or Waiter class
    public static Person RetrieveUser(string username) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM person WHERE username = @username";
      sqlite_cmd.Parameters.AddWithValue("@username", username);
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      Person ret = new Person(rdr.GetInt32(0).ToString(), rdr.GetString(1), rdr.GetString(3), rdr.GetString(4));
      rdr.Close();

      return ret;
    }
  }
}
