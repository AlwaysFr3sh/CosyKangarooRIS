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
      /*RegistrationView registrationView = new RegistrationView("Register");
      LoginView loginView = new LoginView("Login");
      LogoutView logoutView = new LogoutView("logout");
      AddReservationView addreservationView = new AddReservationView("Add Reservation");
      ShowReservationView showreservationview = new ShowReservationView("Show Reservations");
      AddOrderView addOrderView = new AddOrderView("Add Orders");
      InvoiceView createInvoiceView = new InvoiceView("Create Invoice");
      View[] views = new View[] {registrationView, loginView, logoutView, addreservationView, showreservationview, addOrderView, createInvoiceView};
      MainMenu.AddView(views);
      MainMenu.Display();*/

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

      AddReservationView addReservationView = new AddReservationView("Add Reservation");
      ShowReservationView showReservationView = new ShowReservationView("View Reservations");
      RemoveReservationView removeReservationView = new RemoveReservationView("Cancel Reservation");
      AddOrderView addOrderView = new AddOrderView("Add Orders");

      // Arange Views
      View[] loggedOutViews = new View[] {loginView, registrationView};
      View[] waiterViews =  new View[] {logoutView, addReservationView, showReservationView, 
                                        removeReservationView, showMenuView, addMenuItemView, addOrderView};
      View[] customerViews = new View[] {logoutView, addReservationView, showReservationView, 
                                          removeReservationView, showMenuView, addOrderView};

      MainMenu.InitializeViews(loggedOutViews, waiterViews, customerViews);
      MainMenu.Display();
    }
  }
}
