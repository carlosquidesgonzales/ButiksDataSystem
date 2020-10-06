using System;
using System.Collections.Generic;

namespace ButiksDataSystem.Enteties
{
    public class Receipt
    {
        #region Properties
        public DateTime ReceiptId { get; set; }
        public List<ReceiptItem> SelectedItems { get; } = new List<ReceiptItem>();
        public decimal Discount { get; set; }
        public decimal ItemsTotal { get; set; }
        public decimal Total { get; set; }
        public string TotalAmount
        {
            get
            {
                return Total.ToString("#,0.00");               
            }
        }
        #endregion
        #region Methods
        private decimal GetTotal()
        {
            decimal total = 0m;
            foreach (ReceiptItem item in SelectedItems)
            {
                total += item.TotalPrice;
            }
            return total;
        }
        public void UpdateReceipt(List<ReceiptItem> item)
        {

            item.RemoveAll(i => !SelectedItems.Contains(i));
            SetDiscountAndtotal(GetTotal());
        }
        public void SetReceipt(ReceiptItem receiptItem, DateTime receiptId)
        {
            var item = SelectedItems.Find(s => s.ReceiptItemNr == receiptItem.ReceiptItemNr);
            if (item != null)
            {
                item.Quantity += receiptItem.Quantity;
                item.TotalPrice += receiptItem.TotalPrice;
            }
            else
                SelectedItems.Add(receiptItem);

            ReceiptId = receiptId;
            SetDiscountAndtotal(GetTotal());
        }
        private void SetDiscountAndtotal(decimal total)
        {
            decimal discount = 0m;
            decimal newTotal = 0m;
            decimal itemsTotal = 0m;
            if (total > 1000 && total < 2000)
            {
                discount = 0.02m * total;
                itemsTotal = total;
                newTotal = total - discount;
            }
            else if (total > 2000)
            {
                discount = 0.03m * total;
                itemsTotal = total;
                newTotal = total - discount;
            }
            else
            {
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
            if (ItemsTotal != 0)
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
        public void PrintReceiptLimited()
        {
            Console.Clear();
            Console.WriteLine("KASSA");
            Console.WriteLine($"KVITTO  {ReceiptId}");
            Console.WriteLine($"TOTAL: {TotalAmount}");
        }
    }
    #endregion
}
