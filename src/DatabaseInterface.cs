using System;
using System.Data;
using System.Data.SQLite;
using CosyKangaroo.Application;

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

    public static bool RowExists(string table, string rowName, string rowValue) {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = $"SELECT {rowName} FROM {table} WHERE {rowName} = $rowValue;";
      //sqlite_cmd.Parameters.AddWithValue("$table", table);
      //sqlite_cmd.Parameters.AddWithValue("$rowName", rowName);
      sqlite_cmd.Parameters.AddWithValue("$rowValue", rowValue);
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      rdr.Read();
      //string ret = rdr.GetValue(0).ToString();
      // the ? signifies that ret is nullable, this is to silence the warnings!!!
      string? ret = rdr.GetValue(0).ToString();

      rdr.Close();
      return ret == rowValue;
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

    /*public static void ShowReservations() {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM reservations";
      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      while (rdr.Read()){
        ReadSingleRow((IDataRecord)rdr);
      }
      rdr.Close();
    }*/
    // New Show Reservations
    public static void ShowReservations() {
      SQLiteCommand sqlite_cmd;
      sqlite_cmd = sqlite_conn.CreateCommand();
      sqlite_cmd.CommandText = "SELECT * FROM reservations";

      using SQLiteDataReader rdr = sqlite_cmd.ExecuteReader();
      List<List<string>> table = GetTableData(rdr);
      rdr.Close();

      DisplayTableData("CosyKangaroo Restaurant Reservations", table);
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

    // Returns table data in 2d list of string (Should we be using DataTable here?)
    private static List<List<string>> GetTableData(SQLiteDataReader rdr) {
      List<List<string>> ret = new List<List<string>>(); 
      
      // Add column data
      while (rdr.Read()) {
        List<string> listy = new List<string>();
        for (int i=0; i<rdr.FieldCount; i++) {
          string item = "";
          listy.Add(item + rdr.GetValue(i).ToString());
        }
        ret.Add(listy);
      }

      // Add column names
      List<string> columnNames = new List<string>();
      for (int i=0; i<rdr.FieldCount; i++) {
        columnNames.Add(rdr.GetName(i));
      }
      // insert column names at the beginning of the table
      ret.Insert(0, columnNames);

      return ret;
    }

    // Draw a table
    public static void DisplayTableData(string title, List<List<string>> table) {
      // Get the longest string from our table, this will be our column width
      // TODO: do this for each column individually
      int columnWidth = table.SelectMany( i => i).Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur).Length;

      // Draw titlebar across top of table, dynamically pad sides evenly
      // Probably a better way to do this lol
      int paddingRight = ((columnWidth*table[0].Count) + table[0].Count);
      int paddingLeft = (((columnWidth*table[0].Count) + table[0].Count) / 2) + (title.Length/2);
      title = title.PadLeft(paddingLeft, '=');
      title = title.PadRight(paddingRight, '=');
      Console.WriteLine(title);

      foreach (List<string> row in table) {
        // Append whitespace to strings to fit column width
        for (int i=0; i < row.Count; i++) {
          //Console.Write(row[i] + (" " * (columnWidth - row[i].Length)) + "|");
          Console.Write(row[i].PadRight(columnWidth) + "|");
        }
        // Clean way to do new line
        Console.WriteLine("");
      }
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
      /*while (rdr.Read()){
        ReadSingleRow((IDataRecord)rdr);
      }*/
      List<List<string>> table = GetTableData(rdr);
      DisplayTableData("Cosy Kangaroo Orders ", table);
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
  }
}
