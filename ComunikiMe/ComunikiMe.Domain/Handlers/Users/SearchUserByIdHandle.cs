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
    public class SearchUserByIdHandle : Notifiable<Notification>, IHandlerQuery<SearchUserByIdQuery>
    {
        private readonly IUserRepository _userRepository;

        public SearchUserByIdHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryResult Handler(SearchUserByIdQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter user data", query.Notifications);
            }

            var searchedUser = _userRepository.SearchById(query.Id);

            if (searchedUser == null)
            {
                return new GenericQueryResult(false, "Users not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Users found!", searchedUser);
        }
    }
}
