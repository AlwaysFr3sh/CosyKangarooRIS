using System.Collections.Generic;
using CosyKangaroo.Models;
using CosyKangaroo.Presentation;
using CosyKangaroo.Database;

namespace CosyKangaroo {
  class Program {
    static void Main(string[] args) {
      // Initialize database
      DatabaseInterface.OpenDatabaseConnection();

      // testing views
      RegistrationView registrationView = new RegistrationView("Register");
      LoginView loginView = new LoginView("Login");
      LogoutView logoutView = new LogoutView("logout");
      View[] views = new View[] {registrationView, loginView, logoutView};
      MainMenu.AddView(views);
      MainMenu.Display();

      // Close databse
      DatabaseInterface.CloseDatabaseConnection();
    }
  }
}
