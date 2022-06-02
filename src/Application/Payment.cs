using System;

namespace CosyKangaroo.Models {
  class Payment {

    public Payment(decimal amount, PaymentMethod method) {
      Amount = amount;
      Method = method;
    }

    private decimal Amount { get; set; }
    private PaymentMethod Method { get; set; }
    // TODO: "Can Print/Email receipt for customer"
  }
}
