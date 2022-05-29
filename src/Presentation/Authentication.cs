using System;
using CosyKangaroo.Models;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation {
  class RegistrationView : View {
    public RegistrationView(string name) {
      DisplayName = name;
    }
    
    // TODO: Add validation
    public override void Display() {
      Console.Clear();
      // Retrieve Username
      Console.WriteLine("Please enter a username");
      var username =  Console.ReadLine();
      //Retrieve Password
      Console.WriteLine("Please enter a password");
      var password = Console.ReadLine();
      // Retrieve Address
      Console.WriteLine("Please enter an address");
      var address = Console.ReadLine();
      // Retrieve Phone Number
      Console.WriteLine("Please enter a phone number");
      var phone = Console.ReadLine();
      // Register user into the database
      try {
        DatabaseInterface.RegisterUser(new Person(username, address, phone), password);
        Console.WriteLine($"Successfully registered user: {username}");
        Console.ReadLine();
        MainMenu.Display();
      } catch (Exception e) {
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

      Console.WriteLine("Please enter a password");
      var password = Console.ReadLine();

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
