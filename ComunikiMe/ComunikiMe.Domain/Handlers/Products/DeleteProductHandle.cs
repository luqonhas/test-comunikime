using ComunikiMe.Domain.Commands.Products;
using ComunikiMe.Domain.Commands.Users;
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
    public class DeleteProductHandle : Notifiable<Notification>, IHandlerCommand<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICommandResult Handler(DeleteProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the product you want to delete", command.Notifications);
            }

            var searchedProduct = _productRepository.SearchById(command.Id);

            if (searchedProduct == null)
            {
                return new GenericCommandResult(false, "Product not found", command.Notifications);
            }

            _productRepository.Delete(searchedProduct.Id);

            return new GenericCommandResult(false, "Product deleted successfully!", "");
        }
    }
}
