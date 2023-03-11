using ComunikiMe.Domain.Entities;
using ComunikiMe.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Queries.Products
{
    public class SearchProductByIdQuery : Notifiable<Notification>, IQuery
    {
        public Guid Id { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsNotEmpty(Id, "Id", "The 'Id' field cannot be empty!")
                );
        }

        public class SearchUserByIdResult
        {
            public Guid Id { get; set; }
            public string Name { get; private set; }
            public string Description { get; private set; }
            public decimal Price { get; private set; }
            public int Stock { get; private set; }
            public string Image { get; private set; }
            public DateTime InsertDate { get; set; } 
            public DateTime? ModifyDate { get; set; } 

            // Compositions
            public IReadOnlyCollection<Cart> Carts { get; private set; }
            private List<Cart> _carts { get; set; }
        }
    }
}
