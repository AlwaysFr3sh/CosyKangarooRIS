using System;
using CosyKangaroo.Application;
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

      // Differentiate employee from customer
      // (lol very secure)
      Console.WriteLine("Press 'e' to register as an Employee, or 'c' to register as Customer");
      var employeeInput = Console.ReadLine();
      while (employeeInput != "e" && employeeInput != "c") {
        Console.WriteLine("please enter either 'e' or 'c'");
        employeeInput = Console.ReadLine();
      }
      // convert to boolean value
      bool employee = employeeInput == "e";

      // Register user into the database
      try {
        if (employee) 
          DatabaseInterface.RegisterUser(new Waiter(username, address, phone), password);
        else
          DatabaseInterface.RegisterUser(new Customer(username, address, phone), password);

        Console.WriteLine($"Successfully registered user: {username}");
        Console.ReadLine();
        MainMenu.Display();
      } catch (Exception e) {
        Console.WriteLine("That username already exists");
        Console.WriteLine(e);
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
}
