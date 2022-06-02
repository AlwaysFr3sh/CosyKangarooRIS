using System;
using CosyKangaroo.Models;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation {
  class AddOrderView : View {
    public AddOrderView(string name) {
      DisplayName = name;
    }

    public void placeOrder(Table table){
      bool repeat = true;
      int counter = 1;
        while(repeat){
          Console.WriteLine("Enter item ID");
          var itemID = Console.ReadLine();
          while (String.IsNullOrEmpty(itemID)) {
            Console.WriteLine("Item ID cannot be empty");
            itemID = Console.ReadLine();
          }
          var itemIDClean = Convert.ToInt32(itemID);

          Console.WriteLine("Enter item quantity");
          var quantity = Console.ReadLine();
          while (String.IsNullOrEmpty(quantity)) {
            Console.WriteLine("Quantity cannot be empty");
            quantity = Console.ReadLine();
          }
          var quantityClean = Convert.ToInt32(quantity);
          float price = DatabaseInterface.getItemPrice(itemIDClean); //get price from db
          string name = DatabaseInterface.getItemName(itemIDClean); //get name from db
          Order order = new Order(itemIDClean, name, price, quantityClean);
          DatabaseInterface.addOrder(order, table);
          Console.WriteLine($"Successfully placed Order " + counter + " for table " + table.tableNumber);

          Console.WriteLine("Do you wish to place another order?");
          var repeatInput = "";
          while (repeatInput.ToLower() != "y" || repeatInput.ToLower() != "n") {
            repeatInput = Console.ReadLine();
            if(repeatInput.ToLower() == "y"){
              counter++;
              placeOrder(table);
              table.addOrder(order);
            }
            else if(repeatInput.ToLower() == "n"){
              repeat = false;
              break;
            }
            else{
              Console.WriteLine("Invalid input, please enter Y or N");
            }
          }
        }
    }

    public override void Display() {
      Console.Clear();
      Console.WriteLine("Table Number:");
      var tableNumber = Console.ReadLine();
            while (String.IsNullOrEmpty(tableNumber)) {
        Console.WriteLine("Table Number cannot be empty");
        tableNumber = Console.ReadLine();
      }
      var tableNumberClean = Convert.ToInt32(tableNumber);

      Console.WriteLine("Number of Patrons:");
      var numOfPatrons = Console.ReadLine();
                  while (String.IsNullOrEmpty(numOfPatrons)) {
        Console.WriteLine("Number of patrons cannot be empty");
        numOfPatrons = Console.ReadLine();
      }
      var numOfPatronsClean = Convert.ToInt32(numOfPatrons);   

      Table currentTable = new Table(tableNumberClean, numOfPatronsClean, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
      DatabaseInterface.createSitting(currentTable);
      placeOrder(currentTable);

      Console.WriteLine($"Successfully created Order for Table: {tableNumberClean}");
      Console.ReadLine();
      Console.WriteLine("Receipt:");
      currentTable.printReceipt();
      MainMenu.Display();
    }
  }
}
