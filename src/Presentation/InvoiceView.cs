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

      DatabaseInterface.getInvoiceData(tableNumberClean);
      Console.ReadLine();
      MainMenu.Display();
    }
  }
}
