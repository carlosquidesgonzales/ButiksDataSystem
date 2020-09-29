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
        public decimal CampainPrice { get; set; }
        public DateTime? CampainPriceStart { get; set; }
        public DateTime? CampainPriceEnd { get; set; }
        public bool IsCampainPrice()
        {
            double days = 0;
            if (CampainPrice != 0)
            {

                if (CampainPriceEnd != null && CampainPriceStart != null)
                {
                    days = (CampainPriceEnd.Value - CampainPriceStart.Value).TotalDays;
                }
            }
            return days < 0 ? false : true;
        }

    }
}
