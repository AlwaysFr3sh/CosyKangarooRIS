using System.Collections.Generic;
using CosyKangaroo.Application;
using CosyKangaroo.Presentation;
using CosyKangaroo.Database;

namespace CosyKangaroo {
  class Program {
    static void Main(string[] args) {
      // Initialize database
      DatabaseInterface.OpenDatabaseConnection();

      // Start program
      Bootstrap();

      // Close databse
      DatabaseInterface.CloseDatabaseConnection();
    }

    public static void Bootstrap() {
      // Create Views
      LoginView loginView = new LoginView("Login");
      LogoutView logoutView = new LogoutView("Logout");
      RegistrationView registrationView = new RegistrationView("Register");
      AddMenuItemView addMenuItemView = new AddMenuItemView("Add menu Item");
      ShowMenuView showMenuView = new ShowMenuView("View Menu");
      RemoveMenuItemView removeMenuItemView = new RemoveMenuItemView("Remove menu item");
      AddReservationView addReservationView = new AddReservationView("Add Reservation");
      ShowReservationView showReservationView = new ShowReservationView("View Reservations");
      RemoveReservationView removeReservationView = new RemoveReservationView("Cancel Reservation");
      AddOrderView addOrderView = new AddOrderView("Add Orders");
      InvoiceView invoiceView = new InvoiceView("Invoice View");

      // Arange Views
      View[] loggedOutViews = new View[] {registrationView, loginView, showMenuView};
      View[] waiterViews =  new View[] {logoutView, addReservationView, showReservationView, 
                                        removeReservationView, showMenuView, addMenuItemView, removeMenuItemView, addOrderView, invoiceView};
      View[] customerViews = new View[] {logoutView, addReservationView, showReservationView, 
                                          removeReservationView, showMenuView, addOrderView, invoiceView};

      MainMenu.InitializeViews(loggedOutViews, waiterViews, customerViews);
      MainMenu.Display();
    }
  }
}
