using ComunikiMe.Domain.Commands.Products;
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

namespace ComunikiMe.Domain.Handlers.Products
{
    public class UpdateProductHandle : Notifiable<Notification>, IHandlerCommand<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICommandResult Handler(UpdateProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Enter the data correctly", command.Notifications);
            }

            Product oldProduct = _productRepository.SearchById(command.Id);

            if (oldProduct == null)
            {
                return new GenericCommandResult(false, "There is no products with this id", command.Notifications);
            }

            oldProduct.UpdateProduct(command.Name, command.Description, command.Price, command.Stock, command.Image);

            if (!oldProduct.IsValid)
            {
                return new GenericCommandResult(false, "Enter the data correctly", oldProduct.Notifications);
            }

            _productRepository.Update(oldProduct);

            return new GenericCommandResult(true, "Product updated successfully!", oldProduct);
        }
    }
}
