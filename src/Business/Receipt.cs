using System;

namespace CosyKangaroo.Models {
  // Will need to be extended
  class Receipt {
    public Receipt() {

    }
    private string SaleID { get; set; }
    private Order ReceiptOrder { get; set; }
  }
}
