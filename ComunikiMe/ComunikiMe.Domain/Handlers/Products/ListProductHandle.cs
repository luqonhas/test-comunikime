using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Domain.Queries.Products;
using ComunikiMe.Shared.Handlers.Contracts;
using ComunikiMe.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Handlers.Products
{
    public class ListProductHandle : Notifiable<Notification>, IHandlerQuery<ListProductQuery>
    {
        private readonly IProductRepository _productRepository;

        public ListProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IQueryResult Handler(ListProductQuery query)
        {
            var list = _productRepository.List();

            return new GenericQueryResult(true, "Product found!", list);
        }
    }
}
