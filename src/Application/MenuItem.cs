
namespace CosyKangaroo.Application {
  public class MenuItem {
    public MenuItem(string name, decimal price) {
      Name = name;
      Price = price;
    }

    public string Name { get; set;}
    public decimal Price { get; set; }
  }
}
