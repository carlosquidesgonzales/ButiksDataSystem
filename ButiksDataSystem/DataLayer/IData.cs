using ButiksDataSystem.Enteties;
using System.Linq;

namespace ButiksDataSystem.DataLayer
{
    public interface IData
    {
        IQueryable<Product> GetProducts();
        Product FindSingleProduct(int id);
        void Update(string newProduct, string oldPruduct);
        public void Create(Product product);
        void Delete(int id);      
    }
}
