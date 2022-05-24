using System;

namespace CosyKangaroo.Models {
  // will need to be extended
  class Reservation {
    public Reservation() {

    }
     private string ReservationID { get; set; }
     private Customer ReservationCustomer { get; set; }
     private Waiter ReservationWaiter { get; set; }
     private DateTime ReservationTime { get; set; }
  }
}
