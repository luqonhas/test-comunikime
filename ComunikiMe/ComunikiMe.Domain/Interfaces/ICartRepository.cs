using ComunikiMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Interfaces
{
    public interface ICartRepository
    {
        // Commands:
        void Add(Cart cart);

        void Delete(Guid id);



        // Queries:
        IEnumerable<Cart> List();

        Cart SearchById(Guid id);
    }
}
