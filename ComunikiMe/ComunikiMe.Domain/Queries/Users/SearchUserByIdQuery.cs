using ComunikiMe.Domain.Entities;
using ComunikiMe.Shared.Enums;
using ComunikiMe.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Queries.Users
{
    public class SearchUserByIdQuery : Notifiable<Notification>, IQuery
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
            public string UserName { get; private set; }
            public string Password { get; private set; }
            public EnUserType Permission { get; private set; }
            public DateTime InsertDate { get; set; }

            // Compositions
            public IReadOnlyCollection<Cart> Carts { get; private set; }
            private List<Cart> _carts { get; set; }
        }

    }
}
