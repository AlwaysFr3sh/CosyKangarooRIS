namespace CosyKangaroo.Models { 
 class Person {
    public Person(string id, string name, string address, string phoneNumber) {
      ID = id;
      Name = name;
      Address = address;
      PhoneNumber = phoneNumber;
    }

    // Default Constructor, I think this needs to be here?
    public Person() {
      ID = "0";
      Name = "None";
      Address = "Default Address";
      PhoneNumber = "0";
    }

    protected string ID { get; set; }
    protected string Name { get; set; } 
    protected string Address { get; set; }
    protected string PhoneNumber { get; set; }

    public string GetID() {
      return ID;
    }

    public string GetName() {
      return Name;
    }

    public string GetAddress() {
      return Address;
    }

    public string GetPhoneNumber() {
      return PhoneNumber;
    }
  }

  class Customer : Person {
    public Customer(string id, string name, string address, string phoneNumber) {
      ID = id;
      Name = name;
      Address = address;
      PhoneNumber = phoneNumber;
    }

    // TODO: Implement later, don't know how of if this will work right now.  
    public decimal GetBalance() {
      return 0.0m;
    }
  }

  class Waiter : Person {
    public Waiter(string id, string name, string address, string phoneNumber, List<string> tables) {
      ID = id;
      Name = name;
      Address = address;
      PhoneNumber = phoneNumber;
      Tables = tables;
    }

    public Waiter(string id, string name, string address, string phoneNumber) {
      ID = id;
      Name = name;
      Address = address;
      PhoneNumber = phoneNumber;
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
