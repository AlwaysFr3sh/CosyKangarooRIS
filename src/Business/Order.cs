using System;

namespace CosyKangaroo.Models {

  public struct Order
  {
    public Order(int id, string name, float price, int quantity){
      ID = id;
      Name = name;
      Price = price;
      Quantity = quantity;
    }
    public int ID {get;}
    public string Name{get;}
    public float Price{get;}
    public int Quantity{get;}
  }
}
