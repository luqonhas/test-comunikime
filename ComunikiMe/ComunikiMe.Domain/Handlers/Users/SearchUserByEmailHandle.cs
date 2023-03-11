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
    public class SearchUserByEmailHandle : Notifiable<Notification>, IHandlerQuery<SearchUserByEmailQuery>
    {
        private readonly IUserRepository _userRepository;

        public SearchUserByEmailHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IQueryResult Handler(SearchUserByEmailQuery query)
        {
            query.Validate();

            if (!query.IsValid)
            {
                return new GenericQueryResult(false, "Correctly enter user data", query.Notifications);
            }

            var searchedUser = _userRepository.SearchByEmail(query.Email);

            if (searchedUser == null)
            {
                return new GenericQueryResult(false, "User not found", query.Notifications);
            }

            return new GenericQueryResult(true, "Users found!", searchedUser);
        }
    }
}
