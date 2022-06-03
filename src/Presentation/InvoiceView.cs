using System;
using CosyKangaroo.Application;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation {
  class InvoiceView : View {
    public InvoiceView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      Console.Clear();
      Console.WriteLine("Table Number:");
      var tableNumber = Console.ReadLine();
            while (String.IsNullOrEmpty(tableNumber)) {
        Console.WriteLine("Table number cannot be empty");
        tableNumber = Console.ReadLine();
      }
      var tableNumberClean = Convert.ToInt32(tableNumber);
      var listOfOrders = new List<List<string>>();

      listOfOrders = DatabaseInterface.getInvoiceData(tableNumberClean);

      var listOfNames = new List<string>();
      var listOfPrices = new List<string>();

      for(int i = 0; i < listOfOrders.Count; i++){
        listOfNames.Add(DatabaseInterface.getItemName(Convert.ToInt32(listOfOrders[0][i])));
        Console.WriteLine("Order");
        Console.WriteLine(listOfOrders[0][i]);
        Console.WriteLine("name");
        Console.WriteLine(listOfNames[i]);
      }



      Console.WriteLine(listOfOrders.Count);
      Console.WriteLine("past");

      Console.WriteLine(listOfOrders[0][0]);
      Console.WriteLine(listOfOrders[0][1]);
      Console.WriteLine(listOfOrders[0][2]);
      Console.WriteLine(listOfOrders[0][3]);
      

      Console.ReadLine();
      MainMenu.Display();
    }
  }
}
