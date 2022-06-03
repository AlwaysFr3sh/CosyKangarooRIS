using System;
namespace CosyKangaroo.Application
{
    public class PaymentMethod
    {
        public PaymentMethod(float total)
        {
            Total = total;
        }
        public float Total { get; set; }

    }

    class CardDetails : PaymentMethod
    {
        string CardPin;
        string CardNumber;

        public CardDetails(string pCardNumber, string pCardPin, float total) : base(total)
        {
            CardPin = pCardPin;
            CardNumber = pCardNumber;
            Total = total;
        }

        public void payByCard()
        {
            Console.WriteLine("Successful Payment of " + Total + " on " + CardNumber);
        }
    }

    class Cash : PaymentMethod
    {
      float amountTendered;
        public Cash(float total, float amounttendered) : base(total)
        {
            Total = total;
            amountTendered = amounttendered;
        }

        public void payByCash(){
          float change = amountTendered - Total;
          Console.WriteLine("Change: " + change);
        }
    }
}
