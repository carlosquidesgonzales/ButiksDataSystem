using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Menus
{
    public class AdminMenu
    {
        private ReceiptData _rData = new ReceiptData();
        private IData _bl = new Data();
        private void ExceptionMethod(Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            ShowAdminMenu();
        }
        public void CreateProduct()
        {
            var productId = "";
            while (true)
            {
                Console.Write("Product id: ");
                productId = Console.ReadLine();
                var item = _bl.FindSingle(productId);
                if (item == null)//Check id if exist
                    break;
                else
                    Console.WriteLine("Product already exist! Try new Id.");
            }
            var product = EnterProductInfo(productId);
            try
            {
                _bl.Create(product);
            }
            catch (Exception ex)
            {
                ExceptionMethod(ex);
            }

        }
        public Product EnterProductInfo(string productId)
        {

            decimal _price;
            PriceType priceType;
            Console.Write("Product Name: ");
            var productName = Console.ReadLine();

            while (true)
            {
                Console.Write("Price: ");
                var price = decimal.TryParse(Console.ReadLine(), out _price);
                if (price)
                    break;
                else
                    Console.WriteLine("Only decimal");
            }

            while (true)
            {
                Console.WriteLine("Product Type 1 = kilo, 2 = piece:");
                priceType = Enum.Parse<PriceType>(Console.ReadLine());
                if (priceType.Equals(PriceType.kilo) || priceType.Equals(PriceType.piece))
                    break;
                else
                    Console.WriteLine("Choose price type between 1 and 2");
            }
            return new Product { ProductId = productId, ProductName = productName, Price = Convert.ToDecimal(_price), PriceType = priceType };
        }
        public void DeleteProduct()
        {
            var id = "";
            Console.Write("Product Id:");
            id = Console.ReadLine();
            try
            {
                _bl.Delete(id);
            }
            catch (Exception ex)
            {
                ExceptionMethod(ex);
            }
        }
        public void UpdateProduct()
        {
            Console.WriteLine("\nEnter product Id: ");
            string productId = Console.ReadLine();
            try
            {
                var itemToUpdate = _bl.FindSingle(productId);
                //if (itemToUpdate == null) return;//Check if id exist!
                Console.WriteLine($"{itemToUpdate.ProductName}, Price: {itemToUpdate.Price}, Price Type: {itemToUpdate.PriceType}");
                var product = EnterProductInfo(productId);
                Console.WriteLine("Are you sure you want to update? Y/N");
                string processUpdate = Console.ReadLine().ToUpper();
                if (processUpdate != "Y") return;
                string oldProduct = $"{productId}>{itemToUpdate.ProductName}>{itemToUpdate.Price}>{(int)itemToUpdate.PriceType}";
                string newProduct = $"{product.ProductId}>{product.ProductName}>{Convert.ToDecimal(product.Price)}>{(int)product.PriceType}";
                _bl.Update(newProduct, oldProduct);//Update product
            }
            catch (Exception ex)
            {
                ExceptionMethod(ex);
            }

        }
        private void SearchReceipt()
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
                    _rData.GetReceipt(receiptData);
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
            Console.WriteLine("ADMIN MENU");
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
                        CreateProduct();
                        openMenu = false;
                        break;
                    case 2:
                        UpdateProduct();
                        openMenu = false;
                        break;
                    case 3:
                        DeleteProduct();
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
