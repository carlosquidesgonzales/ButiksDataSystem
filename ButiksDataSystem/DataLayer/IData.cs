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
    }
}
