using ButiksDataSystem.Enteties;
using ButiksDataSystem.Enums;
using ButiksDataSystem.Exeptions;
using ButiksDataSystem.Extension_Methods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ButiksDataSystem.DataLayer
{
    public class Data : IData
    {
        private string _path = @"C:\Users\carlo\OneDrive\Skrivbord\C#-2020\C#-Labs\OOP\ButiksDataSystem\ButiksDataSystem\ProductFile\Products.txt";
        #region Methods
        public IQueryable<Product> GetProducts()
        {
            try
            {
                var products = new List<Product>();
                string s = "";
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(_path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        Product pro;
                        string[] product = s.Split(">");
                        int productId = Convert.ToInt32(product[0].Trim());
                        string productName = product[1];
                        decimal price = Convert.ToDecimal(product[2]);
                        PriceType priceType = Enum.Parse<PriceType>(product[3].Trim());
                        int maxQuantity = Convert.ToInt32(product[7]);                      
                        pro = new Product(productId, productName, price, priceType, maxQuantity);
                        pro.CampainPrice = product[4] == "0" ? decimal.Zero : Convert.ToDecimal(product[4]);
                        pro.CampainPriceStart = product[5] == "" ? (DateTime?)null : Convert.ToDateTime(product[5]);
                        pro.CampainPriceEnd = product[6] == "" ? (DateTime?)null : Convert.ToDateTime(product[6]);
                        products.Add(pro);
                    }
                }
                return products.AsQueryable();
            }
            catch
            {
                return null;
            }
        }
        public Product FindSingleProduct(int id)
        {
            try
            {
                var products = GetProducts().ToList();
                return products.FirstOrDefault(p => p.ProductId.Equals(id));
            }
            catch (Exception ex)
            {
                throw new EntityException("Could not find item.", ex);
            }
        }

        public void Update(string newproduct, string oldProduct)
        {
            try
            {
                string products = File.ReadAllText(_path);
                products = products.Replace(oldProduct, newproduct);
                File.WriteAllText(_path, products);
                Console.WriteLine("Product is succesfully updated!");
            }
            catch (Exception ex)
            {

                throw new EntityException("Could not update item.", ex);
            }
        }
        public void Delete(int id)
        {
            try
            {
                var products = GetProducts().ToList();
                var productsFromFile = File.ReadAllLines(_path).ToList();
                int index = products.FindIndex(p => p.ProductId.Equals(id));
                productsFromFile.RemoveAt(index);
                File.WriteAllLines(_path, productsFromFile.ToArray());
                Console.WriteLine("Product succesfully removed!");
            }
            catch (Exception ex)
            {
                throw new EntityException("Could not delete item", ex);
            }
        }
        public void Create(Product product)
        {
            try
            {
                string campainPrice = product.CampainPrice.ToString();
                string campainPriceStart = product.CampainPriceStart.ToString().Split(" ")[0];
                string campainPriceEnd = product.CampainPriceEnd.ToString().Split(" ")[0];
                if(product.CampainPrice == 0)
                {
                    campainPrice = product.CampainPrice.ToString();
                    campainPriceStart = product.CampainPriceStart.ToString();
                    campainPriceEnd = product.CampainPriceEnd.ToString();
                }
                string productTosave = $"\n{product.ProductId}>{product.ProductName}>{product.Price}>" +
                    $"{((int)product.PriceType).ToString()}>{campainPrice}>{campainPriceStart}>{campainPriceEnd}>{product.MaxQuantity}";
                productTosave.AppendToFile(_path);
                File.WriteAllLines(_path, File.ReadAllLines(_path).Where(l => !string.IsNullOrWhiteSpace(l)));
                Console.WriteLine("Product is succesfully created!");
            }
            catch (Exception ex)
            {
                throw new EntityException("Could not create item", ex);
            }
        }
        #endregion
    }
}
