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
      DatabaseInterface.RegisterUser(new Person(username, address, phone), password);
      // Acknowledge
      Console.WriteLine($"Successfully registered user: {username}");
      Console.ReadLine();
      // Return to main menu
      MainMenu.Display();
    }
  }

  class LoginView : View {
    public LoginView(string name) {
      DisplayName = name;
    }

    public override void Display() {
      Console.Clear();
      Console.WriteLine("Please enter a username");
      var username = Console.ReadLine();
      Console.WriteLine("Please enter a password");
      var password = Console.ReadLine();
      // TODO: retrieve username from database, match password and assign logged in user
      MainMenu.Display();
    }
  }

  class LogoutView : View {
    public LogoutView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      Console.Clear();
      Console.WriteLine("Successfully logged out.");
      // Wait for user to acknowledge
      Console.ReadLine();
      MainMenu.Display();
    }
  }
}
