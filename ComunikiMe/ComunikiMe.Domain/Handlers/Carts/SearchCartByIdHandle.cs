using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Domain.Queries.Carts;
using ComunikiMe.Domain.Queries.Users;
using ComunikiMe.Shared.Handlers.Contracts;
using ComunikiMe.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Handlers.Carts
{
    public class SearchCartByIdHandle : Notifiable<Notification>, IHandlerQuery<SearchCartByIdQuery>
    {
        private readonly ICartRepository _cartRepository;

        public SearchCartByIdHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public IQueryResult Handler(SearchCartByIdQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter cart data", query.Notifications);
            }

            var searchedCart = _cartRepository.SearchById(query.Id);

            if (searchedCart == null)
            {
                return new GenericQueryResult(false, "Cart not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Cart found!", searchedCart);
        }
    }
}
