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
    public class ReceiptData
    {
        private Receipt _receipt = new Receipt();
        //Save receipt in a text file
        public void SaveReceipt(Receipt receipt)
        {
            var receiptItem = FormatReceiptItem(receipt);
            var receiptFileName = receipt.ReceiptId.ToString("yyyy-MM-dd");
            //File name for receipt
            var path = $"{receiptFileName}.txt";
            if (!File.Exists(path)) //Check if file does not exist
            {
                //Create a file and save receipt
                try
                {
                    using (StreamWriter sw = File.CreateText(path))//Create file and save receipt
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
        private StringBuilder FormatReceiptItem(Receipt receipt)
        {
            StringBuilder recieptItem = new StringBuilder(receipt.ReceiptId.ToString("MM/dd/yyyy HH:mm:ss"));
            var item = receipt.SelectedItems.ToList();
            for (var i = 0; i < item.Count; i++)
            {
                var order = item[i];
                var newOrderDetails = order.CampainPrice == 0 ? $"{order.ProductName} {order.Quantity}*0 {order.Price}={order.TotalPrice}" 
                    : $"{order.ProductName} {order.Quantity}*{order.CampainPrice} {order.Price}={order.TotalPrice}";
                recieptItem.Append(i == 0 ? $">{newOrderDetails}" : $"|{newOrderDetails}");
            }
            recieptItem.Append($">{receipt.ItemsTotal.ToString("#,0.00")}");
            recieptItem.Append($">{receipt.Discount.ToString("#,0.00")}");
            recieptItem.Append($">{receipt.Total.ToString("#,0.00")}");
            return recieptItem;
        }
        public void GetReceipt(string receiptItem)
        {

            //receipt.SelectedItems.Clear();
            var productItems = new List<OrderRow>();
            var items = receiptItem.Split(">");
            DateTime receiptId = Convert.ToDateTime(items[1], CultureInfo.InvariantCulture);
            var products = items[2].Split("|");
            foreach (var item in products)
            {
                var oRow = new OrderRow();
                var newItem = item.Replace("*", " ").Replace("=", " ").Split(" ").Where(i => !string.IsNullOrEmpty(i)).ToList();
                oRow.ProductName = newItem[0];
                oRow.Quantity = Convert.ToInt32(newItem[1]);
                oRow.CampainPrice = Convert.ToDecimal(newItem[2]);
                oRow.Price = Convert.ToDecimal(newItem[3]);
                oRow.TotalPrice = Convert.ToDecimal(newItem[4]);
                productItems.Add(oRow);
            }
            _receipt.SetReceipt(productItems, receiptId);
            _receipt.PrintReceipt();
        }
        public string FindSingleReceipt(string receiptIdDate, string receiptIdNumber)
        {
            string path = $"{receiptIdDate.Trim()}.txt";
            if (File.Exists(path)) //Check if file does not exist
            {
                var receiptItems = File.ReadAllLines(path).ToList();
                var receipItem = receiptItems.FirstOrDefault(r => r.Split(">")[0].Equals(receiptIdNumber));
                return receipItem != null ? receipItem : null;//return null if not found          
            }
            else
            {
                return null;//return null if not found  
            }
        }
    }
}
