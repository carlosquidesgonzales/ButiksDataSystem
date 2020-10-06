using ButiksDataSystem.Enteties;
using System.Linq;

namespace ButiksDataSystem.DataLayer
{
    public interface IData<T> where T : Product
    {
        IQueryable<T> GetProducts();
        T FindSingleProduct(int id);
        void Update(string newProduct, string oldPruduct);
        public void Create(T product);
        void Delete(int id);      
    }
}
