using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Enteties
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }

        private IData bl = new Data();
        //public Product(string productId, string productName, decimal price, PriceType priceType)
        //{
        //    ProductId = productId;
        //    ProductName = productName;
        //    Price = price;
        //    PriceType = priceType;
        //}
        public void UpdateProduct()
        {
            Console.WriteLine("\nEnter product Id: ");
            string productId = Console.ReadLine();

            var itemToUpdate = bl.FindSingle(productId);

            if (itemToUpdate == null) return;

            Console.WriteLine($"Product Name:{itemToUpdate.ProductName}, Price: {itemToUpdate.Price}, Price Type: {itemToUpdate.PriceType}");

            Console.WriteLine("New Product Name:");
            string newProductName = Console.ReadLine();
            Console.WriteLine("New Price:");
            decimal newPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("New Product Type 1 = kilo, 2 = piece:");
            PriceType newPriceType = Enum.Parse<PriceType>(Console.ReadLine());

            Console.WriteLine("Are you sure you want to update? Y/N");
            string processUpdate = Console.ReadLine().ToUpper();
            if (processUpdate != "Y") return;

            var newUpdatedItem = new Product() { ProductId = productId, ProductName = newProductName, Price = newPrice, PriceType = newPriceType };
            bl.Update(newUpdatedItem);
        }
    }
}
