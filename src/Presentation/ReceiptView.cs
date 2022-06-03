using System;
using CosyKangaroo.Application;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation
{
    class ReceiptView : View
    {
        public ReceiptView(string name)
        {
            DisplayName = name;
        }
        public override void Display()
        {
          /*  Console.Clear();
            Console.WriteLine("Do you wish to pay with card or cash?");
            var repeatInput = "";
            while (repeatInput.ToLower() != "card" || repeatInput.ToLower() != "cash")
            {
                repeatInput = Console.ReadLine();
                if (repeatInput.ToLower() == "card")
                {                  Console.WriteLine("Please enter card number:");
                  string CardNumber = Console.ReadLine();
                  Console.WriteLine("Please enter card pin");
                  string CardPin = Console.ReadLine();
                  Console.WriteLine("Successful Payment of " + Total + " on " + CardNumber);
                  
                }
                else if (repeatInput.ToLower() == "cash")
                {
                  Console.WriteLine("Amount tendered");
                  float amountTendered = float.Parse(Console.ReadLine());
                  float change = amountTendered - Total;
                  Console.WriteLine("Change: " + change);
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter Y or N");
                }
            }*/
            MainMenu.Display();
        }
    }
}
