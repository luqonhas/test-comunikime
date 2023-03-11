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
    public class CreateProductHandle : Notifiable<Notification>, IHandlerCommand<CreateProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandle(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public ICommandResult Handler(CreateProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly enter product data", command.Notifications);
            }

            Product newProduct = new Product(command.Name, command.Description, command.Price, command.Stock, command.Image);

            if (!newProduct.IsValid)
            {
                return new GenericCommandResult(false, "Invalid product data", newProduct.Notifications);
            }

            _productRepository.Add(newProduct);

            return new GenericCommandResult(true, "Product created successfully!", newProduct);
        }
    }
}
