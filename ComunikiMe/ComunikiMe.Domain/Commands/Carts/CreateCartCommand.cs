using ComunikiMe.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Commands.Carts
{
    public class CreateCartCommand : Notifiable<Notification>, ICommand
    {
        public CreateCartCommand() { }

        public CreateCartCommand(Guid idUser, Guid idProduct)
        {
            IdUser = idUser;
            IdProduct = idProduct;
        }

        // Compositions
        public Guid IdUser { get; set; }
        public Guid IdProduct { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(IdUser, "IdUsers", "The 'IdUsers' field cannot be empty!")
                .IsNotEmpty(IdProduct, "IdProducts", "The 'IdProducts' field cannot be empty!")
            );
        }
    }
}
