using System;

namespace CosyKangaroo.classes {
  class Order {
    public Order() {
      OrderItems = new Dictionary<string, decimal>();
      OrderTime = DateTime.Now;
    }

    public Order(Dictionary<string, decimal> _OrderItems) {
      OrderItems = _OrderItems;
    }
    
    // dictionary of item : price
    private Dictionary<string, decimal> OrderItems { get; set; } 

    private DateTime OrderTime { get; set; }

    // Returns Total cost of Order
    public decimal OrderTotal() {
      return OrderItems.Values.Sum();
    }

    // Returns collection of order items
    public List<string> GetOrderItems() {
      return OrderItems.Keys.ToList();
    }

    // Add Order Item
    public void AddItem(string ItemName, decimal ItemPrice) {
      OrderItems.Add(ItemName, ItemPrice);
    }

    // Remove Order Item
    // WARNING, I am assuming that this will remove all items with specified name
    public void RemoveItem(string ItemName) {
      OrderItems.Remove(ItemName);
    }
  }
}
