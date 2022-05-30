using System;

namespace CosyKangaroo.Models {
  // Will need to be extended
  class Receipt {
    public Receipt(string saleID, Order receiptOrder) {
      SaleID = saleID;
      ReceiptOrder = receiptOrder;
    }
    private string SaleID { get; set; }
    private Order ReceiptOrder { get; set; }
  }
}
