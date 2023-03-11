using ComunikiMe.Domain.Commands.Carts;
using ComunikiMe.Domain.Entities;
using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Shared.Commands;
using ComunikiMe.Shared.Handlers.Contracts;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Handlers.Carts
{
    public class CreateCartHandle : Notifiable<Notification>, IHandlerCommand<CreateCartCommand>
    {
        private readonly ICartRepository _cartRepository;

        public CreateCartHandle(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public ICommandResult Handler(CreateCartCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly enter cart data", command.Notifications);
            }

            Cart newCart = new Cart(command.IdUser, command.IdProduct);

            if (!newCart.IsValid)
            {
                return new GenericCommandResult(false, "Invalid cart data", newCart.Notifications);
            }

            _cartRepository.Add(newCart);

            return new GenericCommandResult(true, "Cart created successfully!", newCart);
        }
    }
}
