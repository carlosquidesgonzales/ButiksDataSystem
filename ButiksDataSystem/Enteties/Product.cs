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
        public Product(string productId, string productName, decimal price, PriceType priceType)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            PriceType = priceType;
        }
    }
}
