using ComunikiMe.Domain.Commands.Users;
using ComunikiMe.Domain.Handlers.Users;
using ComunikiMe.Domain.Queries.Users;
using ComunikiMe.Shared.Commands;
using ComunikiMe.Shared.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComunikiMe.Api.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // Commands:

        // register a new user
        [Route("signup")]
        [HttpPost]
        public GenericCommandResult SignUp(CreateUserCommand command, [FromServices] CreateUserHandle handle)
        {
            return (GenericCommandResult)handle.Handler(command);
        }

        // delete a user
        [Route("delete/{id?}")]
        [HttpDelete]
        public GenericCommandResult Delete(Guid id, [FromServices] DeleteUserHandle handle)
        {
            var command = new DeleteUserCommand
            {
                Id = id
            };

            return (GenericCommandResult)handle.Handler(command);
        }



        // Queries:

        // list all users
        [Route("list")]
        [HttpGet]
        public GenericQueryResult List([FromServices] ListUserHandle handle)
        {
            ListUserQuery query = new ListUserQuery();

            return (GenericQueryResult)handle.Handler(query);
        }

        // search user by email
        [Route("searchEmail/{email}")]
        [HttpGet]
        public GenericQueryResult SearchByEmail(string email, [FromServices] SearchUserByEmailHandle handle)
        {

            var query = new SearchUserByEmailQuery
            {
                Email = email
            };

            return (GenericQueryResult)handle.Handler(query);
        }

        // search user by id
        [Route("searchId/{id}")]
        [HttpGet]
        public GenericQueryResult SearchById(Guid id, [FromServices] SearchUserByIdHandle handle)
        {
            var query = new SearchUserByIdQuery
            {
                Id = id
            };

            return (GenericQueryResult)handle.Handler(query);
        }
    }
}
