using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Menus
{
    public class AdminMenu
    {
        private ReceiptData _rData = new ReceiptData();
        public void SearchReceipt()
        {
            while (true)
            {
                Console.WriteLine("Receipt Date year-month-day:");
                var receiptDate = Console.ReadLine();
                Console.WriteLine("Receipt Number:");
                var receiptNumber = Console.ReadLine();
                var receiptData = _rData.FindSingleReceipt(receiptDate, receiptNumber);

                if (receiptData != null)
                {
                    _rData.SetFoundReceipt(receiptData);
                    break;
                }
                else
                {
                    Console.WriteLine("Receipt not found! Try again.");
                }
            }
        }
        public void ShowAdminMenu()
        {
            Console.Clear();
            Console.WriteLine("ADMIN MENU");
            var product = new Product();
            bool openMenu = true;
            while (openMenu)
            {
                Console.WriteLine("1: Create new product");
                Console.WriteLine("2: Update product");
                Console.WriteLine("3: Delete product");
                Console.WriteLine("4: Search receipt");
                Console.WriteLine("0: Exit");
                int number;
                int.TryParse(Console.ReadLine(), out number);
                switch (number)
                {
                    case 1:

                        openMenu = false;
                        break;
                    case 2:
                        product.UpdateProduct();
                        openMenu = false;
                        break;
                    case 3:

                        openMenu = false;
                        break;
                    case 4://Find receipt
                        SearchReceipt();
                        openMenu = false;
                        break;
                    case 0:
                        Environment.Exit(0);
                        openMenu = false;
                        break;
                    default:
                        Console.WriteLine("Invalid menu. Choose between 0 and 3");
                        break;
                }
            }
        }
    }
}
