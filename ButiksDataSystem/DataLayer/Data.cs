using ButiksDataSystem.Enteties;
using ButiksDataSystem.Enums;
using ButiksDataSystem.Exeptions;
using ButiksDataSystem.Extension_Methods;
using ButiksDataSystem.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public class Data : IData
    {
        private string _path = @"C:\Users\carlo\OneDrive\Skrivbord\C#-2020\C#-Labs\OOP\ButiksDataSystem\ButiksDataSystem\ProductFile\Products.txt";
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
                        var pro = new Product();
                        string[] product = s.Split(">");
                        pro.ProductId = product[0].Trim();
                        pro.ProductName = product[1];
                        pro.Price = Convert.ToDecimal(product[2]);
                        pro.PriceType = Enum.Parse<PriceType>(product[3].Trim());
                        pro.CampainPrice = product[4] == "null" ? 0 : Convert.ToDecimal(product[4]);
                        pro.CampainPriceStart = product[5] == "null" ? (DateTime?)null : Convert.ToDateTime(product[5]);
                        pro.CampainPriceEnd = product[6] == "null" ? (DateTime?)null : Convert.ToDateTime(product[6]);
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
        public Product FindSingle(string id)
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
        public void Delete(string id)
        {
            try
            {
                var products = GetProducts().ToList();
                var index = products.FindIndex(p => p.ProductId.Equals(id));
                var tempProducts = File.ReadAllLines(_path).ToList();
                tempProducts.RemoveAt(index);
                File.WriteAllLines(_path, tempProducts.ToArray());
                Console.WriteLine("Product is succesfully removed!");
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
                var campainPrice = product.CampainPrice == 0 ? "null" : product.CampainPrice.ToString();
                var campainPriceStart = product.CampainPriceStart == null ? "null" : product.CampainPriceStart.ToString();
                var campainPriceEnd = product.CampainPriceEnd == null ? "null" : product.CampainPriceEnd.ToString();
                var productTosave = $"\n{product.ProductId}>{product.ProductName}>{product.Price}>" +
                    $"{((int)product.PriceType).ToString()}>{campainPrice}>{campainPriceStart}>{campainPriceEnd}";
                productTosave.AppendToFile(_path);
                Console.WriteLine("Product is succesfully created!");
            }
            catch (Exception ex)
            {
                throw new EntityException("Could find create item", ex);
            }
        }
    }
}
