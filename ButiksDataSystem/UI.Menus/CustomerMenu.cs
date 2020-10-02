using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.Menus
{
    public class CustomerMenu
    {
        private Data<Product> _data;
        private List<Product> _products => _data.GetProducts().ToList();
        private Receipt _receipt = new Receipt();
        private ReceiptData _rData = new ReceiptData();

        public CustomerMenu(Data<Product> data)
        {
            _data = data;
        }

        public void ShowUserMenu()
        {
            Console.WriteLine("CUSTOMER MENU");
            bool openMenu = true;
            while (openMenu)
            {
                Console.WriteLine("1: New Customer");
                Console.WriteLine("0: Exit");
                int number;
                int.TryParse(Console.ReadLine(), out number);
                switch (number)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("KASSA");
                        NewCustomer();
                        openMenu = false;
                        break;
                    case 0:
                        Environment.Exit(0);
                        openMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid menu. Choose between 0 and 1");
                        break;
                }
            }
        }
        public void NewCustomer()
        {
            while (true)
            {
                Console.Write("Komandon: ");
                string input = Console.ReadLine();
                if (input.ToUpper() == "PAY")//Check if the cutomer wants to pay
                {
                    Console.Clear();
                    if (_receipt.SelectedItems.Count > 0)//Check if basket is not empty
                    {
                        try
                        {
                            _receipt.PrintReceipt();
                            _rData.SaveReceipt(_receipt);
                            _receipt.SelectedItems.Clear();
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                            NewCustomer();
                        }
                    }
                    else
                    {
                        Console.WriteLine("KASSA");
                        Console.WriteLine("Your basket is empty.");
                    }
                    Console.Write("<productId> <antal>\n");
                    Console.WriteLine("PAY");//Save to text file
                }
                else if (input != "")//Check if the customer did not picked any product to buy
                {
                    string[] product = input.Split(" ");
                    if (input == "" || input.ToUpper() == "EXIT")//
                    {
                        Console.Clear();
                        Console.WriteLine("KASSA");
                        Console.WriteLine("Operation canceled.");
                        ShowUserMenu();
                    }
                    if (product.Length > 1)
                    {
                        CheckIfExist(product);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("KASSA");
                        Console.Write("<productId> <antal>\n");
                        Console.WriteLine("Please enter a valid value.");
                    }
                }
            }
        }
        private void CheckIfExist(string[] input)
        {
            
            var receiptItem = new ReceiptItem();
            int id = Convert.ToInt32(input[0].Trim());
            var receiptId = DateTime.Now;
            var product = _products.SingleOrDefault(p => p.ProductId == id);
            if (product != null) //Check if item exist
            {
                int quantity;
                int.TryParse(input[1].Trim(), out quantity);
                var singleReceipt = _receipt.SelectedItems.FirstOrDefault(r => r.ReceiptItemNr == id);
                if(singleReceipt != null)
                {
                    if(singleReceipt.Quantity + quantity > product.MaxQuantity)
                    {
                        Console.WriteLine($"Only {product.MaxQuantity} pieces/kilos of {product.ProductName} allowed. Try again!");
                        return;
                    }                       
                }
                if (quantity == 0) //Check if the quantity is a valid input
                {
                    Console.WriteLine("Enter a valid quantity");
                    return;
                }
                receiptItem.ReceiptItemNr = product.ProductId;
                receiptItem.ProductName = product.ProductName;
                receiptItem.Price = product.Price;
                receiptItem.CampainPrice = product.IsCampainPrice() ? product.CampainPrice : 0;
                receiptItem.TotalPrice = product.IsCampainPrice() ? quantity * product.CampainPrice : product.Price * quantity;
                receiptItem.Quantity = quantity;
                _receipt.SetReceipt(receiptItem, receiptId);
                _receipt.PrintReceipt();
            }
            else
            {
                Console.WriteLine("Item does not exist! Try again.");
            }
        }
    }
}
