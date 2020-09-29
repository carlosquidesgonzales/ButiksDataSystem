using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ButiksDataSystem.Enteties
{
    public class OrderRow//: Product
    {
        
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public decimal? CampainPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderDetails { 
            get 
            {
                var total = TotalPrice.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("sv-SE"));
                var price = CampainPrice != 0 ? $"({Price}) {CampainPrice.Value}" : $"{Price}";
                return $"{ProductName} {Quantity} * {price} = {total}";
            } 
        }
    }
}
