namespace CosyKangaroo.Models { 
 public class Person {
    public Person(string id, string name, string address, string phone) {
      ID = id;
      Name = name;
      Address = address;
      Phone = phone;
    }

    // Doesn't have an ID yet when being registered to database
    // lowkey don't like how this works
    public Person(string name, string address, string phone) {
      ID = "";
      Name = name;
      Address = address;
      Phone = phone;
    }

    // Default Constructor, I think this needs to be here?
    public Person() {
      ID = "0";
      Name = "None";
      Address = "Default Address";
      Phone = "0";
    }

    protected string ID { get; set; }
    protected string Name { get; set; } 
    protected string Address { get; set; }
    protected string Phone { get; set; }

    public string GetID() {
      return ID;
    }

    public string GetName() {
      return Name;
    }

    public string GetAddress() {
      return Address;
    }

    public string GetPhone() {
      return Phone;
    }
  }

  class Customer : Person {
    public Customer(string id, string name, string address, string phone) {
      ID = id;
      Name = name;
      Address = address;
      Phone = phone;
    }

    // TODO: Implement later, don't know how or if this will work right now.  
    public decimal GetBalance() {
      return 0.0m;
    }
  }

  class Waiter : Person {
    public Waiter(string id, string name, string address, string phone, List<string> tables) {
      ID = id;
      Name = name;
      Address = address;
      Phone = phone;
      Tables = tables;
    }

    public Waiter(string id, string name, string address, string phone) {
      ID = id;
      Name = name;
      Address = address;
      Phone = phone;
      Tables = new List<string>();
    }

    // List of table numbers the waiter is serving.
    private List<string> Tables { get; set; }

    public List<string> GetTables() {
      return Tables;
    }

    public void AddTable(string tableNumber) {
      Tables.Add(tableNumber);
    }

    public void RemoveTable(string tableNumber) {
      Tables.Remove(tableNumber);
    }
  }
}
