using ButiksDataSystem.DataLayer;
using ButiksDataSystem.Enteties;
using System.Collections.Generic;
using System.Linq;

namespace ButiksDataSystem
{
    class Program
    {
        static IData bl = new Data();
        static void Main(string[] args)
        {
          
            var items = bl.GetProducts().ToList();
           
        }
    }
}
