using ButiksDataSystem.DataLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ButiksDataSystem.Enteties
{
    public class Receipt
    {
        public DateTime ReceiptId { get; set; }
        public List<OrderRow> SelectedItems { get; } = new List<OrderRow>();
        public decimal Discount { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal Total {get; set;}
        public string TotalAmount{ 
            get
            {
                return Total.ToString("#,0.00");
            }
        }
        private decimal GetTotal()
        {
            decimal total = 0m;
            foreach (OrderRow item in SelectedItems)
            {
                total += item.TotalPrice;
            }
            return total;
        }
        public void SetReceipt(List<OrderRow> orderRow, DateTime receiptId)
        {
            SelectedItems.AddRange(orderRow);
            ReceiptId = receiptId;
            SetDiscountAndtotal(GetTotal());
        }
        private void SetDiscountAndtotal(decimal total)
        {
            decimal discount = 0;
            decimal newTotal = 0;
            decimal itemsTotal = 0;
            if (total > 1000 && total < 2000)
            {
                var d = 0.02m * total;
                discount = 0.02m * total;
                itemsTotal = total;
                newTotal = total -d;
            }else if (total > 2000)
            {
                var d = 0.03m * total;
                discount = 0.03m * total;
                itemsTotal = total;
                newTotal = total -d;
            }
            else
            {
                discount = 0m;
                itemsTotal = 0m;
                newTotal = total;
            }
            ItemsTotal = itemsTotal;
            Discount = discount;
            Total = newTotal;
        }
        public void PrintReceipt()
        {
            Console.Clear();
            Console.WriteLine("KASSA");
            Console.WriteLine($"KVITTO  {ReceiptId}");
            foreach (var item in SelectedItems)
            {
                Console.WriteLine(item.OrderDetails);
            }
            if(ItemsTotal != 0)
            {
                Console.WriteLine($"ITEMS TOTAL: {ItemsTotal.ToString("#,0.00")} kr");
                Console.WriteLine($"Discount: -{Discount.ToString("#,0.00")} kr");
                Console.WriteLine($"TOTAL: {TotalAmount} kr");
            }
            else
            {
                Console.WriteLine($"TOTAL: {TotalAmount}");
            }
            
        }      
    }
}
