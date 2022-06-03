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
      var listOfOrders = new List<string>();

      listOfOrders = DatabaseInterface.getInvoiceData(tableNumberClean);

      var listOfNames = new List<string>();
      var listOfPrices = new List<float>();
      int j = 0;
        Console.Write("Quantity\t");
        Console.Write("Item\t");
        Console.WriteLine("Price");
      for(int i = 0; i < listOfOrders.Count; i+=2, j++){
        listOfNames.Add(DatabaseInterface.getItemName(Convert.ToInt32(listOfOrders[i])));
        float individualPrice = DatabaseInterface.getItemPrice(Convert.ToInt32(listOfOrders[i]));
        listOfPrices.Add(individualPrice*float.Parse(listOfOrders[i+1]));

        Console.Write(listOfOrders[i+1]+"\t");
        Console.Write(listOfNames[j]+"\t");
        Console.WriteLine(listOfPrices[j]);
      }
      float total = listOfPrices.Sum(x => x);
      Console.Write("Total: " + total);

      Console.ReadLine();
      MainMenu.Display();
    }
  }
}
