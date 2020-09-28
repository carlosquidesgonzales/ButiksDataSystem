using ButiksDataSystem.Enteties;
using ButiksDataSystem.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public class Data : IData
    {


        public IQueryable<Product> GetProducts()
        {
            try
            {
                string path = @"ProductFile\Products.txt";
                var products = new List<Product>();
                string s = "";
                string productId = "";
                string productName = "";
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
                        products.Add(new Product() { ProductId = productId, ProductName = productName, Price = price, PriceType = priceType });
                    }
                }
                return products.AsQueryable();
            }
            catch
            {
                throw;
            }
        }
        public Product FindSingle(string id)
        {
            var products = GetProducts();
            try
            {
                return products.FirstOrDefault(p => p.ProductId.Equals(id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Update<T>(T entity) where T : class
        {

        }
    }
}
