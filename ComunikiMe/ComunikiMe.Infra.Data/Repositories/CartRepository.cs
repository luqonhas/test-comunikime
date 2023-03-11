using ComunikiMe.Domain.Entities;
using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Infra.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ComunikiMeContext _ctx;

        public CartRepository(ComunikiMeContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(Cart cart)
        {
            _ctx.Carts.Add(cart);
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _ctx.Carts.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<Cart> List()
        {
            return _ctx.Carts
                .AsNoTracking()
                .ToList();
        }

        public Cart SearchById(Guid id)
        {
            return _ctx.Carts
                .Include(x => x.Users)
                .Include(x => x.Products)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
