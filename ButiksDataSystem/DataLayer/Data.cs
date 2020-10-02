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
    public class Data<T> : IData<T> where T : Product , new()
    {
        private string _path = @"C:\Users\carlo\OneDrive\Skrivbord\C#-2020\C#-Labs\OOP\ButiksDataSystem\ButiksDataSystem\ProductFile\Products.txt";
        public IQueryable<T> GetProducts()
        {
            try
            {
                var products = new List<T>();
                string s = "";
                // Open the file to read from.
                using (StreamReader sr = File.OpenText(_path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        var pro = new T();
                        string[] product = s.Split(">");
                        pro.ProductId = Convert.ToInt32(product[0].Trim());
                        pro.ProductName = product[1];
                        pro.Price = Convert.ToDecimal(product[2]);
                        pro.PriceType = Enum.Parse<PriceType>(product[3].Trim());
                        pro.CampainPrice = product[4] == "0" ? decimal.Zero : Convert.ToDecimal(product[4]);
                        pro.CampainPriceStart = product[5] == "" ? (DateTime?)null : Convert.ToDateTime(product[5]);
                        pro.CampainPriceEnd = product[6] == "" ? (DateTime?)null : Convert.ToDateTime(product[6]);
                        pro.MaxQuantity = Convert.ToInt32(product[7]);
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
        public T FindSingle(int id)
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
                int index = products.FindIndex(p => p.ProductId.Equals(id));
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
        public void Create(T product)
        {
            try
            {
                string campainPrice = product.CampainPrice.ToString();
                string campainPriceStart = product.CampainPriceStart.Value.ToString();
                string campainPriceEnd = product.CampainPriceEnd.Value.ToString();
                if(product.CampainPrice == 0)
                {
                    campainPrice = null;
                    campainPriceStart = "null";
                    campainPriceEnd = "null";

                }
                string productTosave = $"\n{product.ProductId}>{product.ProductName}>{product.Price}>" +
                    $"{((int)product.PriceType).ToString()}>{campainPrice}>{campainPriceStart}>{campainPriceEnd}>{product.MaxQuantity}";
                productTosave.AppendToFile(_path);
                Console.WriteLine("Product is succesfully created!");
            }
            catch (Exception ex)
            {
                throw new EntityException("Could not create item", ex);
            }
        }
    }
}
