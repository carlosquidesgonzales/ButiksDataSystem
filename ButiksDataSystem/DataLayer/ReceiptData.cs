using ButiksDataSystem.Enteties;
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
        public void SaveReceipt(DateTime receiptId, List<OrderRow> orderRow, decimal total)
        {
            var receiptItem = FormatReceiptItem(receiptId, orderRow, total);//Get receipt items
            var receiptFileName = receiptId.ToString("yyyy-MM-dd");
            //File name for receipt
            var path = receiptFileName + ".txt";
            if (!File.Exists(path)) //Check if file does not exist
            {
                //Create a file and save receipt
                try
                {
                    using (StreamWriter sw = File.CreateText(path))//Create file and save receipt
                    {
                        sw.WriteLine("1>" + receiptItem);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else //If file exist
            {
                int lines = File.ReadAllLines(path).Length;
                int lineIndex = lines == 0 ? 1 : lines + 1;
                try
                {
                    using (StreamWriter sw = File.AppendText(path))  //Append new receipt in the same file
                    {
                        sw.WriteLine((lineIndex).ToString() + ">" + receiptItem);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        //Format receipt into a readable line saparating deatials in , and product items in |
        private StringBuilder FormatReceiptItem(DateTime receiptId, List<OrderRow> orderRow, decimal total)
        {
            StringBuilder recieptItem = new StringBuilder(receiptId.ToString("MM/dd/yyyy HH:mm:ss"));
            for(var i = 0; i < orderRow.Count; i++)
            {
                recieptItem.Append(i == 0 ? ">" + orderRow[i].OrderDetails : "|" + orderRow[i].OrderDetails);
            }
            recieptItem.Append(">" + total.ToString());
            return recieptItem;
        }
        public void SetFoundReceipt(string receiptItem)
        {
            
            //receipt.SelectedItems.Clear();
            var productItems = new List<OrderRow>();
            var items = receiptItem.Split(">");
            //DateTime.ParseExact(items[1], "yyyy-MM-dd HH:mm:ss", null);
            DateTime receiptId = Convert.ToDateTime(items[1], CultureInfo.InvariantCulture);
            var products = items[2].Split("|");           
            foreach (var item in products)
            {
                var newItem = item.Replace("*", "").Replace("=", "").Split(" ").Where(i => !string.IsNullOrEmpty(i)).ToList();
                var productName = newItem[0];
                var quantity = Convert.ToInt32(newItem[1]);
                var price = Convert.ToDecimal(newItem[2]);
                var totalPrice = Convert.ToDecimal(newItem[3]);
                productItems.Add(new OrderRow { ProductName = productName, Price = price, Quantity = quantity, TotalPrice = totalPrice });
            }
            _receipt.SetReceipt(productItems, receiptId);
            _receipt.PrintReceipt();
        }
        public string FindSingleReceipt(string receiptIdDate, string receiptIdNumber)
        {
            string path = receiptIdDate.Trim() + ".txt";
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
