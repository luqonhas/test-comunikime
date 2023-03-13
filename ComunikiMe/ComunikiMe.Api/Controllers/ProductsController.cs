using ComunikiMe.Domain.Commands.Products;
using ComunikiMe.Domain.Commands.Users;
using ComunikiMe.Domain.Handlers.Products;
using ComunikiMe.Domain.Handlers.Users;
using ComunikiMe.Shared.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ComunikiMe.Api.Controllers
{
    [Route("v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // Commands:

        [Route("register")]
        [HttpPost]
        public GenericCommandResult Register(CreateProductCommand command, [FromServices] CreateProductHandle handle)
        {
            return (GenericCommandResult)handle.Handler(command);
        }

        [Route("delete/{id?}")]
        [HttpDelete]
        public GenericCommandResult Delete(Guid id, [FromServices] DeleteProductHandle handle)
        {
            var command = new DeleteProductCommand
            {
                Id = id
            };

            return (GenericCommandResult)handle.Handler(command);
        }
    }
}