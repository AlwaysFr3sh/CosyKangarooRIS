using System.Collections.Generic;
using CosyKangaroo.classes;

namespace CosyKangaroo {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Hello, World!");
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
