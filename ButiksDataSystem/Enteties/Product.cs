using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ButiksDataSystem.Enteties
{
    public class Product
    {
        #region Properties
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public PriceType PriceType { get; set; }
        public decimal CampainPrice { get; set; }
        public DateTime? CampainPriceStart { get; set; }
        public DateTime? CampainPriceEnd { get; set; }
        public int MaxQuantity { get; set; }
        #endregion
        #region Methods
        public Product(int productId, string productName, decimal price, PriceType priceType, int maxQuantity)
        {
            ProductId = productId;
            ProductName = productName;
            Price = price;
            PriceType = priceType;
            MaxQuantity = maxQuantity;
        }
        public bool IsCampainPrice()
        {
            bool isCampain = false;
            if (CampainPrice != 0)
            {
                if (CampainPriceEnd != null && CampainPriceStart != null)
                {
                    var dates = GetBetweenDates(CampainPriceStart.Value, CampainPriceEnd.Value);
                    var day = dates.FirstOrDefault(d => d == DateTime.Now.Date);
                        isCampain = DateTime.Now.Date == day ? true : false;
                }
            }
            return isCampain;
        }
        private List<DateTime> GetBetweenDates(DateTime start, DateTime end)
        {
            var dates = new List<DateTime>();

            for (var dt = start; dt <= end; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }
            return dates;
        }
        #endregion
    }
}
