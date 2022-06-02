using System;

namespace CosyKangaroo.Application {
  // Will need to be extended
  class Receipt {
    public Receipt() {
    }
    public void printReceipt(Table table){
      Console.WriteLine("Date: " + table.date + "\t Time: " + table.time);
      Console.WriteLine("Table: " + table.tableNumber);
      
      foreach(Order order in table.completeOrder){
        Console.WriteLine(order.Name + "\t" + order.Price + " x" + order.Quantity);
      }
      Console.WriteLine("Total Price: " + table.getTotalPrice());
    }
  }
}
