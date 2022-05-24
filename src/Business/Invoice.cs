using System;

namespace CosyKangaroo.Models {
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
}
