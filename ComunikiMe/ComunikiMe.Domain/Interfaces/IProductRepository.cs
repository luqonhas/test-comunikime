using ComunikiMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Interfaces
{
    public interface IProductRepository
    {
        // Commands:
        void Add(Product product);

        void Delete(Guid id);

        void Update(Product product);



        // Queries:
        IEnumerable<Product> List();

        Product SearchById(Guid id);
    }
}
