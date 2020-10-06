using ButiksDataSystem.Enteties;
using ButiksDataSystem.Exeptions;
using ButiksDataSystem.Extension_Methods;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public class ReceiptData<T>: IReceiptData<T> where T: Receipt
    {
        private Receipt _receipt = new Receipt();
        #region Methods
        public void SaveReceipt(T receipt)//Save receipt in a text file
        {
            var receiptItem = FormatReceiptItem(receipt);
            string receiptFileName = receipt.ReceiptId.ToString("yyyy-MM-dd");
            //File name for receipt
            string path = $"{receiptFileName}.txt";
            if (!File.Exists(path)) //Check if file does not exist
            {             
                try
                {
                    using (StreamWriter sw = File.CreateText(path)) //Create a file for receipt
                    {
                        sw.WriteLine($"1>{receiptItem}");
                    }
                }
                catch (Exception ex)
                {
                    throw new EntityException("Could not save receipt.", ex);
                }
            }
            else //If file exist
            {
                int lines = File.ReadAllLines(path).Length;
                int lineIndex = lines == 0 ? 1 : lines + 1;
                string item = $"{(lineIndex).ToString()}>{receiptItem}";
                item.AppendToFile(path);
            }
        }
        private StringBuilder FormatReceiptItem(T receipt)
        {
            StringBuilder recieptItem = new StringBuilder(receipt.ReceiptId.ToString("MM/dd/yyyy HH:mm:ss"));
            var item = receipt.SelectedItems.ToList();
            for (var i = 0; i < item.Count; i++)
            {
                var order = item[i];
                string newOrderDetails = order.CampainPrice == 0 ? $"{order.ProductName} {order.Quantity}*0 {order.Price}={order.TotalPrice}" 
                    : $"{order.ProductName} {order.Quantity}*{order.CampainPrice} {order.Price}={order.TotalPrice}";
                recieptItem.Append(i == 0 ? $">{newOrderDetails}" : $"|{newOrderDetails}");
            }
            recieptItem.Append($">{receipt.ItemsTotal.ToString("#,0.00")}");
            recieptItem.Append($">{receipt.Discount.ToString("#,0.00")}");
            recieptItem.Append($">{receipt.Total.ToString("#,0.00")}");
            return recieptItem;
        }
        public void GetReceipt(string receiptItem, int choice)
        {
            var productItems = new List<ReceiptItem>();
            string[] items = receiptItem.Split(">");
            DateTime receiptId = Convert.ToDateTime(items[1], CultureInfo.InvariantCulture);
            string[] products = items[2].Split("|");
            foreach (var item in products)
            {
                var newReceiptItem = new ReceiptItem();
                var newItem = item.Replace("*", " ").Replace("=", " ").Split(" ").Where(i => !string.IsNullOrEmpty(i)).ToList();
                newReceiptItem.ProductName = newItem[0];
                newReceiptItem.Quantity = Convert.ToInt32(newItem[1]);
                newReceiptItem.CampainPrice = Convert.ToDecimal(newItem[2]);
                newReceiptItem.Price = Convert.ToDecimal(newItem[3]);
                newReceiptItem.TotalPrice = Convert.ToDecimal(newItem[4]);
                _receipt.SetReceipt(newReceiptItem, receiptId);
            }
            if (choice == 1)
                _receipt.PrintReceiptLimited();
            else
                _receipt.PrintReceipt();
        }
        public string FindSingleReceipt(string receiptIdDate, string receiptIdNumber)
        {
            string path = $"{receiptIdDate.Split(" ")[0].Trim()}.txt";
            if (File.Exists(path)) //Check if file does not exist
            {
                var receiptItems = File.ReadAllLines(path).ToList();
                string receipItem = receiptItems.FirstOrDefault(r => r.Split(">")[0].Equals(receiptIdNumber));
                return receipItem != null ? receipItem : null;//return null if not found          
            }
            else
            {
                return null;//return null if not found  
            }
        }
        #endregion
    }
}
