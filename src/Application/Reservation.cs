using System;

namespace CosyKangaroo.Application {
  // will need to be extended
  public class Reservation {
    public Reservation(string CustomerName, int Patrons, string Date, string Time) {
      ReservationCustomer = CustomerName;
      ReservationPatrons = Patrons;
      ReservationDate = Date;
      ReservationTime = Time;
    }

    public Reservation() {
      ReservationCustomer = "Customer";
      ReservationPatrons = 0;
      ReservationDate = "Null";
      ReservationTime = "Null";
    }
     private string ReservationCustomer { get; set; }
     private int ReservationPatrons { get; set; }
     private string ReservationDate { get; set; }
     private string ReservationTime { get; set; }

    public string GetReservationCustomer() {
      return ReservationCustomer;
    }

    public int GetReservationPatrons() {
      return ReservationPatrons;
    }

    public string GetReservationDate() {
      return ReservationDate;
    }

    public string GetReservationTime() {
      return ReservationTime;
    }




  }
}
