using System.Collections.Generic;
using CosyKangaroo.Application;
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
      AddReservationView addreservationView = new AddReservationView("Add Reservation");
      ShowReservationView showreservationview = new ShowReservationView("Show Reservations");
      AddOrderView addOrderView = new AddOrderView("Add Orders");
      View[] views = new View[] {registrationView, loginView, logoutView, addreservationView, showreservationview, addOrderView};
      MainMenu.AddView(views);
      MainMenu.Display();

      // Close databse
      DatabaseInterface.CloseDatabaseConnection();
    }
  }
}
