using System;

namespace CosyKangaroo.Application {
  class PaymentMethod {
    public PaymentMethod() {

    }
  }

  class CardDetails : PaymentMethod {
    string CardName;
		int CardNumber;
		
		public CardDetails(string pCardName, int pCardNumber) {
			CardName = pCardName;
			CardNumber = pCardNumber;
		}
  }

  class Cash : PaymentMethod {
    public Cash() {

    }
  }
}
