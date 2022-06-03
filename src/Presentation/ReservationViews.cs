using System;
using CosyKangaroo.Application;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation {
  class AddReservationView : View {
    public AddReservationView(string name) {
      DisplayName = name;
    }
    public override void Display() {
      Console.Clear();

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
}
