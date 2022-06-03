using System;

namespace CosyKangaroo.Application {
  class Invoice {
    public Invoice(string saleID, int tableNumber, List<string> items, float total) {
      SaleID = saleID;
      TableNumber = tableNumber;
      ItemList = items;
      Total = total;

    }
    private string SaleID { get; set;}
    private int TableNumber { get; set; }
    private List<string> ItemList { get; set; }
    private float Total { get; set; }
  }
}
