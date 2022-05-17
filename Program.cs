using System.Collections.Generic;

namespace CosyKangaroo {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Hello, World!");

      Order myOrder = new Order();
      myOrder.AddItem("penis", 100.6m);
      myOrder.AddItem("butthole", 69.6m);
      myOrder.AddItem("vagina", 10.0m);
      Console.WriteLine(myOrder.OrderTotal());
      Console.WriteLine(string.Join(" ", myOrder.GetOrderItems()));
    }
  }

  class Invoice {
    public Invoice() {

    }
  }

  class Receipt {
    public Receipt() {

    }
  }

  class Reservation {
    public Reservation() {

    }
  }

  class Payment {
    public Payment() {

    }
  }

  class PaymentMethod {
    public PaymentMethod() {

    }
  }

  class CardDetails : PaymentMethod {
    public CardDetails() {

    }
  }

  class Cash : PaymentMethod {
    public Cash() {

    }
  }
}
