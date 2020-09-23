using ButiksDataSystem.Enteties;
using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public class Data:IData
    {
        public IQueryable<Product> GetProducts()
        {
            try
            {
                string path = @"C:\Users\carlo\OneDrive\Skrivbord\C#-2020\C#-Labs\OOP\ButiksDataSystem\ButiksDataSystem\ProductData\Products.txt";
                var products = new List<Product>();
                string s = "";
                string productId;
                string productName;
                decimal price = 0;
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(path))
                {
                    PriceType priceType;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] product = s.Split("-");
                        productId = product[0].Trim();
                        productName = product[1];
                        price = Convert.ToDecimal(product[2]);
                        priceType = Enum.Parse<PriceType>(product[3].Trim());
                        //priceType = (PriceType)Enum.Parse(typeof(PriceType), product[3].Trim(), true);
                        products.Add(new Product(productId, productName, price, priceType));
                    }
                }
                return products.AsQueryable();
            }
            catch
            {
                throw;
            }
        }
    }
}
