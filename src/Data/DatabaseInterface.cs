using System;
using System.Data;
using System.Data.SQLite;
using CosyKangaroo.Application;
using CosyKangaroo.Utilities;

namespace CosyKangaroo.Database {
  // Method to get database version (for testing)
  public static class DatabaseInterface {

    public static string ConnectionString = "DataSource=Data/mydatabase.db;Version=3;New=True;Compress=True;";
    public static SQLiteConnection sqlite_conn = new SQLiteConnection(ConnectionString);

    public static void OpenDatabaseConnection() {
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
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      string version = rdr.GetString(0);
      rdr.Close();

      return version;
    }

    public static bool RowExists(string table, string rowName, string rowValue) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = $"SELECT {rowName} FROM {table} WHERE {rowName} = $rowValue;";
      sqlite_cmd.Parameters.AddWithValue("$rowValue", rowValue);
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      // the ? signifies that ret is nullable, this is to silence the warnings!!!
      string? ret = rdr.GetValue(0).ToString();

      rdr.Close();
      return ret == rowValue;
    }

    // an assumption I wrote in Assignment2 said we aren't doing encryption so I won't worry about that here - Tom
    public static void RegisterUser(Person user, string password) {
      bool employee = (user is Waiter);

      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "INSERT INTO person (username, password, phone, address, employee) VALUES ($username, $password, $phone, $address, $employee);";
      sqlite_cmd.Parameters.AddWithValue("$username", user.GetName());
      sqlite_cmd.Parameters.AddWithValue("$password", password);
      sqlite_cmd.Parameters.AddWithValue("$phone", user.GetPhone());
      sqlite_cmd.Parameters.AddWithValue("$address", user.GetAddress());
      sqlite_cmd.Parameters.AddWithValue("$employee", employee);
      sqlite_cmd.ExecuteNonQuery();
    }

    // DEPRECATED, use RowExists
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
    public static Person RetrieveUser(string username) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM person WHERE username = @username";
      sqlite_cmd.Parameters.AddWithValue("@username", username);
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      bool employee = rdr.GetBoolean(5);
      Person ret;
      if (employee)
        ret = new Waiter(rdr.GetInt32(0).ToString(), rdr.GetString(1), rdr.GetString(3), rdr.GetString(4));
      else
        ret = new Customer(rdr.GetInt32(0).ToString(), rdr.GetString(1), rdr.GetString(3), rdr.GetString(4));
      rdr.Close();

      return ret;
    }

    // Add reservation
    public static void AddReservation(Reservation reservation) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "INSERT INTO reservations (customerName, patrons, resDate, resTime) VALUES ($customerName, $patrons, $resDate, $resTime);";
      sqlite_cmd.Parameters.AddWithValue("$customerName", reservation.GetReservationCustomer());
      sqlite_cmd.Parameters.AddWithValue("$patrons", reservation.GetReservationPatrons());
      sqlite_cmd.Parameters.AddWithValue("$resDate", reservation.GetReservationDate());
      sqlite_cmd.Parameters.AddWithValue("$resTime", reservation.GetReservationTime());
      sqlite_cmd.ExecuteNonQuery();
    }

    // New Show Reservations
    public static void ShowAllReservations() {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM reservations";

      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      List<List<string>> table = Utils.GetTableData(rdr);
      rdr.Close();

      Utils.DisplayTableData("CosyKangaroo Restaurant Reservations", table);
    }

    public static void ShowReservations(string username) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM reservations WHERE customerName = $username";
      sqlite_cmd.Parameters.AddWithValue("$username", username);

      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      List<List<string>> table = Utils.GetTableData(rdr);
      rdr.Close();

      Utils.DisplayTableData($"Reservations for {username}", table);
    }

    public static void RemoveReservation(string reservationID) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "DELETE FROM reservations WHERE id = $reservationID;";
      sqlite_cmd.Parameters.AddWithValue("$reservationID", reservationID);
      sqlite_cmd.ExecuteNonQuery();
    }
    private static void ReadSingleRow(IDataRecord dataRecord)
    {
        Console.WriteLine(String.Format("{0}, {1}, {2}, {3}, {4}", dataRecord[0], dataRecord[1], dataRecord[2], dataRecord[3], dataRecord[4]));
    }

    //needs to be populated
    public static void createSitting(Table currentTable) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "INSERT INTO TableDetails (TableNumber, patrons, TableDate, TableTime) VALUES ($TableNumber, $patrons, $TableDate, $TableTime);";
      sqlite_cmd.Parameters.AddWithValue("$TableNumber", currentTable.tableNumber);
      sqlite_cmd.Parameters.AddWithValue("$patrons", currentTable.numberOfPatrons);
      sqlite_cmd.Parameters.AddWithValue("$TableDate", currentTable.date);
      sqlite_cmd.Parameters.AddWithValue("$TableTime", currentTable.time);
      sqlite_cmd.ExecuteNonQuery();
    }


    public static void addOrder(Order order, Table table){
      string Status = "Pending";
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "INSERT INTO orders (TableNumber, itemid, quantity, kitchenstatus) VALUES ($TableNumber, $itemid, $quantity, $kitchenstatus);";
      sqlite_cmd.Parameters.AddWithValue("$TableNumber", table.tableNumber);
      sqlite_cmd.Parameters.AddWithValue("$itemid", order.ID);
      sqlite_cmd.Parameters.AddWithValue("$quantity", order.Quantity);
      sqlite_cmd.Parameters.AddWithValue("$kitchenstatus", Status);
      sqlite_cmd.ExecuteNonQuery();
    }

    public static void ShowOrders(){
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM orders";
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      List<List<string>> table = Utils.GetTableData(rdr);
      Utils.DisplayTableData("Cosy Kangaroo Orders ", table);
      rdr.Close();
    }

    public static float getItemPrice(int id){
      float result = 0;
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM item where id = " + id;
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      while(rdr.Read()){
        result = (float)rdr.GetDecimal(2);
      }
      return result;
    } 

    public static string getItemName(int id){
      string result = "";
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM item where id = " + id;
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      while(rdr.Read()){
        result = rdr.GetString(1);
      }
      return result;
    } 

    public static void AddMenuItem(MenuItem item) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "INSERT INTO item (name, price) VALUES ($name, $price);";
      sqlite_cmd.Parameters.AddWithValue("$name", item.Name);
      sqlite_cmd.Parameters.AddWithValue("$price", item.Price);
      sqlite_cmd.ExecuteNonQuery();
    }

    public static void ShowMenu() {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM item;";
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      List<List<string>> table = Utils.GetTableData(rdr);
      rdr.Close();
      Utils.DisplayTableData("Cosy Kangaroo Menu", table);
    }

    public static List<string> getInvoiceData(int tableNumber) {
      var result = new List<string>();
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT itemid, quantity FROM orders where TableNumber = " + tableNumber;
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();

      List<string> sublist = new List<string>();

      while(rdr.Read()){
        result.Add(Convert.ToString(rdr["itemid"]));
        result.Add(Convert.ToString(rdr["quantity"]));
      }
      return result;
    }
  }
}
