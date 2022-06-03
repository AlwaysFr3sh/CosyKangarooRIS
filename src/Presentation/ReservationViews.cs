using System;
using System.Globalization;
using CosyKangaroo.Application;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation {
  class AddReservationView : View {
    public AddReservationView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      Console.Clear();
      /*Console.WriteLine("Customer Name:");
      var customerName = Console.ReadLine();
            while (String.IsNullOrEmpty(customerName)) {
        Console.WriteLine("Customer name cannot be empty");
        customerName = Console.ReadLine();
      }*/
      var customerName = MainMenu.LoggedInUser.GetName(); 

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
      Console.Clear();
      if (MainMenu.WaiterLoggedIn())
        DatabaseInterface.ShowAllReservations();
      else
        DatabaseInterface.ShowReservations(MainMenu.LoggedInUser.GetName());

      Console.WriteLine("Press <Enter> to return");
      Console.ReadLine();
      MainMenu.Display();
    }
  }


  // Currently I am just removing from the database here, however it is also an option to 
  // add a row to the reservations table called "status" or something that we could set to "cancelled" here 
  // instead of straight up removing it, which might be a better solution, however I will leave it like this for now
  class RemoveReservationView : View {
    public RemoveReservationView(string name) {
      DisplayName = name;
    }

    public override void Display() {
      // show reservations to display
      DatabaseInterface.ShowAllReservations();
      Console.WriteLine("Select a Reservation by ID to remove");
      var reservationID = Console.ReadLine();
      // Validate that the given id is a valid int, however we will use the string value in practice
      // TODO: also validate that the selected reservation id actually exists
      while (!int.TryParse(reservationID, out int reservationIDInt)) {
        Console.WriteLine("Please enter a valid Reservation ID");
        reservationID = Console.ReadLine();
      }
      // Check that the user specified row actually exists
      if (!DatabaseInterface.RowExists("reservations", "id", reservationID)) {
        Console.WriteLine("The row you specified does not exist");
      } else {
        // Remove reservation here
        DatabaseInterface.RemoveReservation(reservationID);
        Console.WriteLine($"Removed reservation id: {reservationID}");
      }
      Console.ReadLine();
      MainMenu.Display();
    }
  }

  class AddMenuItemView : View {
    public AddMenuItemView(string name) {
      DisplayName = name;
    }

    public override void Display() {
      Console.Clear();
      Console.WriteLine("Enter a name");
      var name = Console.ReadLine();
      while (String.IsNullOrEmpty(name)) {
        Console.WriteLine("Name cannot be empty");
        name = Console.ReadLine();
      }
      Console.WriteLine("Enter a price");
      var priceString = Console.ReadLine();
      decimal price;
      while (!decimal.TryParse(priceString, NumberStyles.Any, CultureInfo.InvariantCulture, out price)) {
        Console.WriteLine("Price must be number");
        priceString = Console.ReadLine();
      }

      DatabaseInterface.AddMenuItem(new MenuItem(name, price));
      Console.WriteLine("Press <Enter> to return");
      Console.ReadLine();
      MainMenu.Display();
    }
  }

  class ShowMenuView : View {
    public ShowMenuView(string name) {
      DisplayName = name;
    }

    public override void Display() {
      Console.Clear();
      DatabaseInterface.ShowMenu();
      Console.WriteLine("Press <Enter> to return");
      Console.ReadLine();
      MainMenu.Display();
    }
  }
}
