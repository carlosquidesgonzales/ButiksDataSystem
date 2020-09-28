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

        //public decimal Dicount { get; set; }
        //public decimal ItemsTotal { get; set; }
        public decimal Total {get; set;}
        public string TotalAmount{ 
            get{
                return Total.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("sv-SE"));
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
            Total = GetTotal();
        }
        //public Receipt GetReceipt()
        //{
        //    return new Receipt() {ReceiptId = ReceiptId, SelectedItems = SelectedItems, Total = Total };

        //}
        public void PrintReceipt()
        {
            Console.Clear();
            Console.WriteLine("KASSA");
            Console.WriteLine($"KVITTO  {ReceiptId}");
            foreach (var item in SelectedItems)
            {
                Console.WriteLine(item.OrderDetails);
            }
            Console.WriteLine($"TOTAL: {TotalAmount}");
        }      
    }
}
