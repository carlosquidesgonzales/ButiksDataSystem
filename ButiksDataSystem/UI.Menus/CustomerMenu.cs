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
        private IData _data = new Data();
        private Receipt _receipt = new Receipt();
        //private List<OrderRow> _orderRow = new List<OrderRow>();
        private ReceiptData _rData = new ReceiptData();

        public void ShowUserMenu()
        {
            //Console.Clear();
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
                        _receipt.PrintReceipt();
                        _rData.SaveReceipt(_receipt.ReceiptId, _receipt.SelectedItems, _receipt.Total);
                        _receipt.SelectedItems.Clear();
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
                        var start = new StartMenu();
                        Console.Clear();
                        Console.WriteLine("KASSA");
                        Console.WriteLine("Operation cancled");
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
            string id = input[0].Trim();
            var findItem = _data.FindSingle(id);//Find the item if exixt in the list of products
            if (findItem != null) //Check if item exist
            {
                int quantity;
                int.TryParse(input[1].Trim(), out quantity);
                if (quantity == 0) //Check if the quantity is a valid input
                {
                    Console.WriteLine("Enter a valid quantity");
                    return;
                }
                string pName = findItem.ProductName;
                decimal price = findItem.Price;
                decimal totalPrice = quantity * findItem.Price;
                var newOrder = new List<OrderRow>();
                newOrder.Add(new OrderRow { ProductName = pName, Price = price, Quantity = quantity, TotalPrice = totalPrice });
                var receiptId = DateTime.Now;
                _receipt.SetReceipt(newOrder, receiptId);
                _receipt.PrintReceipt();
            }
            else
            {
                Console.WriteLine("Item does not exist! Try again.");
            }
        }
    }
}
