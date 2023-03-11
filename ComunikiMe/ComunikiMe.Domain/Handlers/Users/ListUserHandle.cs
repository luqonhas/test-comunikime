using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Domain.Queries.Users;
using ComunikiMe.Shared.Handlers.Contracts;
using ComunikiMe.Shared.Queries;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Handlers.Users
{
    public class ListUserHandle : Notifiable<Notification>, IHandlerQuery<ListUserQuery>
    {
        private readonly IUserRepository _userRepository;

        public ListUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryResult Handler(ListUserQuery query)
        {
            var list = _userRepository.List();

            return new GenericQueryResult(true, "Users found!", list);
        }
    }
}
