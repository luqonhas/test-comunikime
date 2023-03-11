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
    public class ProductRepository : IProductRepository
    {
        private readonly ComunikiMeContext _ctx;

        public ProductRepository(ComunikiMeContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(Product product)
        {
            _ctx.Products.Add(product);
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _ctx.Products.Remove(SearchById(id));
            _ctx.SaveChanges();
        }

        public void Update(Product product)
        {
            _ctx.Entry(product).State = EntityState.Modified;
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<Product> List()
        {
            return _ctx.Products
                .AsNoTracking()
                .ToList();
        }

        public Product SearchById(Guid id)
        {
            return _ctx.Products.FirstOrDefault(x => x.Id == id);
        }
    }
}
