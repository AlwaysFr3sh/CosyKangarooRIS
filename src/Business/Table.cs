using System;

namespace CosyKangaroo.Models {
    public class Table{
        public Table(int pTableNumber, int pNumberOfPatrons, string pDate, string pTime){
            tableNumber = pTableNumber;
            numberOfPatrons = pNumberOfPatrons;
            date = pDate;
            time = pTime;
        }

        public List<Order> completeOrder = new List<Order>();
        public int tableNumber;
        public int numberOfPatrons;
        public string date;
        public string time;

        public void addOrder(Order Order){
            completeOrder.Add(Order);
        }

        public float getTotalPrice(){
            float totalPrice = 0;
            foreach(Order order in completeOrder){
                totalPrice += order.Price;
            }
            return totalPrice;
        }
        public void printReceipt(){
            Receipt tableReceipt = new Receipt();
            tableReceipt.printReceipt(this);
        }
    }
}