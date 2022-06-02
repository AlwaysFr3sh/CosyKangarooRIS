using System.Data.SQLite;

namespace CosyKangaroo.Utils {
  public static class Util {
    // Returns table data in 2d list of string (Should we be using DataTable here?)
    public static List<List<string>> GetTableData(SQLiteDataReader rdr) {
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
  }
}
