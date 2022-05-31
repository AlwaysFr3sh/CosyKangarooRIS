using System;
using CosyKangaroo.Models;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation {
  class RegistrationView : View {
    public RegistrationView(string name) {
      DisplayName = name;
    }
    
    // TODO(tom): Add elegant validation, I made this to make the warning go away but it sucks.
    public override void Display() {
      Console.Clear();
      // Retrieve Username
      Console.WriteLine("Please enter a username");
      var username =  Console.ReadLine();
      // Validate 
      while (String.IsNullOrEmpty(username)) {
        Console.WriteLine("username cannot be empty, please input a valid username");
        username = Console.ReadLine();
      }

      //Retrieve Password
      Console.WriteLine("Please enter a password");
      var password = Console.ReadLine();
      // Validate 
      while (String.IsNullOrEmpty(password)) {
        Console.WriteLine("password cannot be empty, please input a valid password");
        password = Console.ReadLine();
      }

      // Retrieve Address
      Console.WriteLine("Please enter an address");
      var address = Console.ReadLine();
      // Validate
      while (String.IsNullOrEmpty(address)) {
        Console.WriteLine("address cannot be empty, please input a valid address");
        address = Console.ReadLine();
      }

      // Retrieve Phone Number
      Console.WriteLine("Please enter a phone number");
      var phone = Console.ReadLine();
      while (String.IsNullOrEmpty(phone)) {
        Console.WriteLine("phone number cannot be empty, please input a valid phone number");
        phone = Console.ReadLine();
      }

      // Register user into the database
      try {
        DatabaseInterface.RegisterUser(new Person(username, address, phone), password);
        Console.WriteLine($"Successfully registered user: {username}");
        Console.ReadLine();
        MainMenu.Display();
      } catch {
        Console.WriteLine("That username already exists");
        Console.ReadLine();
        Display();
      }
    }
  }

  class LoginView : View {
    public LoginView(string name) {
      DisplayName = name;
    }

    public override void Display() {
      Console.Clear();
      
      bool userExists, correctPassword;
      Person user;

      Console.WriteLine("Please enter a username");
      var username = Console.ReadLine();
      while (String.IsNullOrEmpty(username)) {
        Console.WriteLine("username cannot be empty");
        username = Console.ReadLine();
      }

      Console.WriteLine("Please enter a password");
      var password = Console.ReadLine();
      while (String.IsNullOrEmpty(password)) {
        Console.WriteLine("password cannot be empty");
      }

      // Check that user exists
      userExists = DatabaseInterface.UserExists(username);

      // Check that password is correct
      correctPassword = DatabaseInterface.AuthenticatePassword(username, password);

      // Retrieve user
      if (correctPassword) {

        user = DatabaseInterface.RetrieveUser(username);
        MainMenu.LogIn(user);
        Console.WriteLine($"Logged in as: {username}");
        Console.ReadLine();

      } else {

        Console.WriteLine("IncorrectPassword");
        Console.ReadLine();
        Display();

      }

      MainMenu.Display();
    }
  }

  class LogoutView : View {
    public LogoutView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      Console.Clear();
      // Log Out
      MainMenu.LogOut();
      // Prompt user
      Console.WriteLine("Successfully logged out.");
      // Wait for user to acknowledge
      Console.ReadLine();
      // Return to main menu
      MainMenu.Display();
    }
  }

  class AddReservationView : View {
    public AddReservationView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      Console.Clear();
      Console.WriteLine("Customer Name:");
      var customerName = Console.ReadLine();
            while (String.IsNullOrEmpty(customerName)) {
        Console.WriteLine("Customer name cannot be empty");
        customerName = Console.ReadLine();
      }
      Console.WriteLine("Number of Patrons:");
      var numOfPatrons = Console.ReadLine();
                  while (String.IsNullOrEmpty(numOfPatrons)) {
        Console.WriteLine("Number of patrons cannot be empty");
        numOfPatrons = Console.ReadLine();
      }
      var numOfPatronsClean = Convert.ToInt32(numOfPatrons);

      Console.WriteLine("Date of Booking");
      var dateOfBooking = Console.ReadLine();
            while (String.IsNullOrEmpty(dateOfBooking)) {
        Console.WriteLine("username cannot be empty");
        dateOfBooking = Console.ReadLine();
      }
      Console.WriteLine("Time of Booking");
      var timeOfBooking = Console.ReadLine();
            while (String.IsNullOrEmpty(timeOfBooking)) {
        Console.WriteLine("username cannot be empty");
        timeOfBooking = Console.ReadLine();
      }

      DatabaseInterface.AddReservation(new Reservation(customerName, numOfPatronsClean, dateOfBooking, timeOfBooking));
      Console.WriteLine($"Successfully created reservation for: {customerName}");
      Console.ReadLine();
      MainMenu.Display();
    }
  }

  class ShowReservationView : View {
    public ShowReservationView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      DatabaseInterface.ShowReservations();
    }
  }

class AddOrderView : View {
    public AddOrderView(string name) {
      DisplayName = name;
    }

    public void placeOrder(Table table){
      bool repeat = true;
      int counter = 1;
        while(repeat){
          var itemID = Console.ReadLine();
          while (String.IsNullOrEmpty(itemID)) {
            Console.WriteLine("Item ID cannot be empty");
            itemID = Console.ReadLine();
          }
          var itemIDClean = Convert.ToInt32(itemID);

          var quantity = Console.ReadLine();
          while (String.IsNullOrEmpty(quantity)) {
            Console.WriteLine("Quantity cannot be empty");
            quantity = Console.ReadLine();
          }
          var quantityClean = Convert.ToInt32(quantity);

          float price = DatabaseInterface.getItemPrice(itemIDClean); //get price from db
          string name = DatabaseInterface.getItemName(itemIDClean); //get name from db
          Order order = new Order(itemIDClean, name, price, quantityClean);

          DatabaseInterface.addOrder(order, table);
          Console.WriteLine($"Successfully placed Order " + counter + " for table " + table.tableNumber);

          var repeatInput = Console.ReadLine();
          Console.WriteLine("Do you wish to place another order?");
          repeatInput = Console.ReadLine();
          if(repeatInput.ToLower() == "y"){
            counter++;
            placeOrder(table);
          }
          if(repeatInput.ToLower() == "n"){
            repeat = false;
          }
          else{
            Console.WriteLine("Invalid input, please enter Y or N");
          }
        }
    }

    public override void Display() {
      Console.Clear();
      Console.WriteLine("Table Number:");
      var tableNumber = Console.ReadLine();
            while (String.IsNullOrEmpty(tableNumber)) {
        Console.WriteLine("Table Number cannot be empty");
        tableNumber = Console.ReadLine();
      }
      var tableNumberClean = Convert.ToInt32(tableNumber);

      Console.WriteLine("Number of Patrons:");
      var numOfPatrons = Console.ReadLine();
                  while (String.IsNullOrEmpty(numOfPatrons)) {
        Console.WriteLine("Number of patrons cannot be empty");
        numOfPatrons = Console.ReadLine();
      }
      var numOfPatronsClean = Convert.ToInt32(numOfPatrons);   

      Table currentTable = new Table(tableNumberClean, numOfPatronsClean, DateTime.Now.ToString("dd/MM/yyyy"), DateTime.Now.ToString("HH:mm"));
      DatabaseInterface.createSitting(currentTable);
      placeOrder(currentTable);

      Console.WriteLine($"Successfully created Order for Table: {tableNumberClean}");
      Console.ReadLine();
      MainMenu.Display();
    }
  }




  class ShowOrdersView : View {
    public ShowOrdersView(string name){
      DisplayName = name;
    }

    public override void Display(){
      DatabaseInterface.ShowOrders();
    }
  }
}
