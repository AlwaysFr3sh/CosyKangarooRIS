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
}
