using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Extension_Methods;
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
        private Data<Product> _bl = new Data<Product>();
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
        public decimal SafeNumberInput()
        {
            decimal number = 0;
            while (true)
            {
                string input = Console.ReadLine();
                if (decimal.TryParse(input, out number) == true)
                    break;
                Console.WriteLine("Only number or a decimal");
            }
            return number;
        }
        public int SafeIntInput()
        {
            int number = 0;
            while (true)
            {
                string input = Console.ReadLine();
                if (Int32.TryParse(input, out number) == true)
                    break;
                Console.WriteLine("Only number or a decimal");
            }
            return number;
        }
        public DateTime SafeDateInput()
        {
            DateTime date;
            while (true)
            {
                string input = Console.ReadLine();
                if (DateTime.TryParse(input, out date) == true)
                    break;
                Console.WriteLine("Only date yyyy-mm-dd");
            }
            return date;
        }
        private void ExceptionMethod(Exception ex)
        {
            Console.Clear();
            Console.WriteLine(ex.Message);
            ShowAdminMenu();
        }
        public void CreateProduct()
        {
            int productId = 0;
            var item = new Product();
            while (true)
            {
                Console.Write("Product id: ");
                productId = SafeIntInput();
                item = _bl.FindSingle(productId);
                if (item == null)//Check id if exist
                    break;
                else
                    Console.WriteLine("Product already exist! Try new product Id.");
            }
            var product = EnterProductInfo(productId, item);
            try
            {
                _bl.Create(product);
            }
            catch (Exception ex)
            {
                ExceptionMethod(ex);
            }

        }
        private Product EnterProductInfo(int productId, Product item)
        {

            decimal _price;
            string productName = "";
            PriceType priceType;
            decimal campainPrice = 0;
            DateTime? campainPriceStart = (DateTime?)null;
            DateTime? campainPriceEnd = (DateTime?)null;
            int maxQuantity = 0;
            while (true)
            {
                Console.Write("Product Name: ");//Input product name
                productName = Console.ReadLine();
                if (item == null && productName == null)
                {
                    Console.WriteLine("Product name cannot be empty");
                }
                else
                {
                    productName = item.ProductName;
                    break;
                }

            }

            Console.Write("Price: ");//Input product price
            _price = SafeNumberInput();
            while (true)
            {
                Console.WriteLine("Product Type 1 = kilo, 2 = piece:"); //Input price type
                priceType = Enum.Parse<PriceType>(Console.ReadLine());
                if (priceType.Equals(PriceType.kilo) || priceType.Equals(PriceType.piece))
                    break;
                else
                    Console.WriteLine("Choose price type between 1 and 2");
            }

            Console.WriteLine("Campain price:");//Input campian price
            campainPrice = SafeNumberInput();
            if (campainPrice != 0)
            {
                Console.WriteLine("Campain start date:");//Input campian start date
                campainPriceStart = SafeDateInput();
                Console.WriteLine("Campain end date:");//Input campian start date
                while (true)
                {
                    campainPriceEnd = SafeDateInput();
                    if ((campainPriceEnd.Value - campainPriceStart.Value).TotalDays < 0)
                        Console.WriteLine("End of campian price cannot happen before start of campain price");
                    else
                        break;
                }
            }
            else
            {
                campainPriceStart = (DateTime?)null;
                campainPriceEnd = (DateTime?)null;
            }
            Console.WriteLine("Max quantity:");//Input for maximum quantity
            maxQuantity = SafeIntInput();
            return new Product
            {
                ProductId = productId,
                ProductName = productName,
                Price = Convert.ToDecimal(_price),
                PriceType = priceType,
                CampainPrice = campainPrice == 0 ? 0 : campainPrice,
                CampainPriceStart = campainPriceStart,
                CampainPriceEnd = campainPriceEnd,
                MaxQuantity = maxQuantity
            };
        }
        public void DeleteProduct()
        {
            string id = "";
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
            int productId = SafeIntInput();
            try
            {
                var itemToUpdate = _bl.FindSingle(productId);
                //Console.WriteLine($"{itemToUpdate.ProductName}, Price: {itemToUpdate.Price}, Price Type: {itemToUpdate.PriceType} Campain price: {itemToUpdate.CampainPrice} Start: {itemToUpdate.CampainPriceStart} End: {itemToUpdate.CampainPriceEnd} Maximun quantity: {itemToUpdate.MaxQuantity}");
                var product = EnterProductInfo(productId, itemToUpdate);              
                Console.WriteLine("Are you sure you want to update? Y/N");
                string processUpdate = Console.ReadLine().ToUpper();
                if (processUpdate != "Y") return;
                string oldProduct = $"{productId}>{itemToUpdate.ProductName}>{itemToUpdate.Price}>" +
                    $"{(int)itemToUpdate.PriceType}>{itemToUpdate.CampainPrice}>" +
                    $"{itemToUpdate.CampainPriceStart.ToString().Split(" ")[0]}>" +
                    $"{itemToUpdate.CampainPriceEnd.ToString().Split(" ")[0]}>{itemToUpdate.MaxQuantity}";
                string newProduct = $"{product.ProductId}>{product.ProductName}>" +
                    $"{product.Price}>{(int)product.PriceType}>" +
                    $"{product.CampainPrice.ToString()}>{product.CampainPriceStart.ToString()}>" +
                    $"{product.CampainPriceEnd.ToString()}>{product.MaxQuantity}";
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
                DateTime receiptDate;
                int receiptNumber = 0;
                while (true)
                {
                    Console.WriteLine("Receipt Date year-month-day:");
                    var date = DateTime.TryParse(Console.ReadLine(), out receiptDate);
                    if (date)
                        break;
                    else
                        Console.WriteLine("Error date input. Try again.");
                }
                while (true)
                {
                    Console.WriteLine("Receipt Number:");
                    var num = Int32.TryParse(Console.ReadLine(), out receiptNumber);
                    if (num)
                        break;
                    else
                        Console.WriteLine("Make sure it is a number.");
                }
                try
                {
                    string receiptData = _rData.FindSingleReceipt(receiptDate.ToString(), receiptNumber.ToString());
                    _rData.GetReceipt(receiptData);
                }
                catch (Exception ex)
                {
                    ExceptionMethod(ex);
                }
            }
        }
    }
}
