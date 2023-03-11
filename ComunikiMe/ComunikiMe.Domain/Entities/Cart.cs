using ComunikiMe.Shared.Entities;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Entities
{
    public class Cart : Base
    {
        public Cart(Guid idUsers, Guid idProducts)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(idUsers, "IdUsers", "The 'IdUsers' field cannot be empty!")
                .IsNotEmpty(idProducts, "IdProducts", "The 'IdProducts' field cannot be empty!")
            );

            if (IsValid)
            {
                IdUsers = idUsers;
                IdProducts = idProducts;
            }
        }

        // FK's
        public Guid IdUsers { get; set; }
        public User Users { get; set; }

        public Guid IdProducts { get; set; }
        public Product Products { get; set; }
    }
}
