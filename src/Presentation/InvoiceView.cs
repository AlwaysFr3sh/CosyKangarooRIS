using System;
using CosyKangaroo.Application;
using CosyKangaroo.Database;

namespace CosyKangaroo.Presentation
{
    class InvoiceView : View
    {
        public InvoiceView(string name)
        {
            DisplayName = name;
        }
        public override void Display()
        {
            Console.Clear();
            Console.WriteLine("Table Number:");
            var tableNumber = Console.ReadLine();
            while (String.IsNullOrEmpty(tableNumber))
            {
                Console.WriteLine("Table number cannot be empty");
                tableNumber = Console.ReadLine();
            }
            var tableNumberClean = Convert.ToInt32(tableNumber);
            var listOfOrders = new List<string>();

            listOfOrders = DatabaseInterface.getInvoiceData(tableNumberClean);

            var listOfNames = new List<string>();
            var listOfPrices = new List<float>();
            int j = 0;
            Console.Write("Quantity\t");
            Console.Write("Item\t");
            Console.WriteLine("Price");
            for (int i = 0; i < listOfOrders.Count; i += 2, j++)
            {
                listOfNames.Add(DatabaseInterface.getItemName(Convert.ToInt32(listOfOrders[i])));
                float individualPrice = DatabaseInterface.getItemPrice(Convert.ToInt32(listOfOrders[i]));
                listOfPrices.Add(individualPrice * float.Parse(listOfOrders[i + 1]));

                Console.Write(listOfOrders[i + 1] + "\t");
                Console.Write(listOfNames[j] + "\t");
                Console.WriteLine(listOfPrices[j]);
            }
            float total = listOfPrices.Sum(x => x);
            Console.Write("Total: " + total);
            var SaleID = Convert.ToString(tableNumber) + Convert.ToString(total);
            new Invoice(SaleID, Convert.ToInt32(tableNumber), listOfOrders, total);
            Console.ReadLine();

            Console.WriteLine("Do you wish to pay now?");
            var repeatInput = "";
            while (repeatInput.ToLower() != "y" || repeatInput.ToLower() != "n")
            {
                repeatInput = Console.ReadLine();
                if (repeatInput.ToLower() == "y")
                {

                    Console.WriteLine("Do you wish to pay with card or cash?");
                    var repeatInput2 = "";
                    while (repeatInput2.ToLower() != "card" || repeatInput2.ToLower() != "cash")
                    {
                        repeatInput2 = Console.ReadLine();
                        if (repeatInput2.ToLower() == "card")
                        {
                            Console.WriteLine("Please enter card number:");
                            string CardNumber = Console.ReadLine();
                            Console.WriteLine("Please enter card pin");
                            string CardPin = Console.ReadLine();
                            CardDetails cardDetials = new CardDetails(CardNumber, CardPin, total);
                            cardDetials.payByCard();
                            break;
                        }
                        else if (repeatInput2.ToLower() == "cash")
                        {
                            Console.WriteLine("Amount tendered");
                            float amountTendered = float.Parse(Console.ReadLine());
                            Cash cash = new Cash(total, amountTendered);
                            cash.payByCash();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input, please enter Cash or Card");
                        }
                    }
                    break;
                }
                else if (repeatInput.ToLower() == "n")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid input, please enter Y or N");
                }
            }
            Console.ReadLine();
            MainMenu.Display();
        }
    }
}
