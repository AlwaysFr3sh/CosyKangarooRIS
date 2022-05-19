using System.Collections.Generic;
using CosyKangaroo.classes;

namespace CosyKangaroo {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Hello, World!");
    }
  }

  class Invoice {
    public Invoice(string saleID, Order invoiceOrder, Customer invoiceCustomer, Waiter invoiceWaiter) {
      SaleID = saleID;
      InvoiceOrder = invoiceOrder;
      InvoiceCustomer = invoiceCustomer;
      InvoiceWaiter = invoiceWaiter;
    }
    private string SaleID { get; set;}
    private Order InvoiceOrder { get; set; }
    private Customer InvoiceCustomer { get; set; }
    private Waiter InvoiceWaiter { get; set; }

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
