using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public interface IData
    {
        IQueryable<Product> GetProducts();
        Product FindSingle(string id);
        void Update(string newProduct, string oldPruduct);
        public void Create(Product product);
        void Delete(string id);
        
    }
}
