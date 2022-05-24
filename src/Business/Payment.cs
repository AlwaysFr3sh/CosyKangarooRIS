using System;

namespace CosyKangaroo.Models {
  class Payment {
    public Payment() {

    }

    private decimal Amount { get; set; }
    private PaymentMethod Method { get; set; }
    // TODO: "Can Print/Email receipt for customer"
  }
}
