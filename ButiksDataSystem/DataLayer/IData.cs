using ButiksDataSystem.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ButiksDataSystem.DataLayer
{
    public interface IData<T> where T : Product
    {
        IQueryable<T> GetProducts();
        T FindSingle(int id);
        void Update(string newProduct, string oldPruduct);
        public void Create(T product);
        void Delete(string id);      
    }
}
