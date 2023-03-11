using ComunikiMe.Domain.Commands.Authentications;
using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Shared.Commands;
using ComunikiMe.Shared.Handlers.Contracts;
using ComunikiMe.Shared.Utils;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Handlers.Authentications
{
    public class LoginUserNameHandle : Notifiable<Notification>, IHandlerCommand<LoginUserNameCommand>
    {
        private readonly IUserRepository _UserRepository;

        public LoginUserNameHandle(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }

        public ICommandResult Handler(LoginUserNameCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Enter the data correctly", command.Notifications);
            }

            var searchedUser = _UserRepository.SearchByUserName(command.UserName);

            if (!Password.ValidateHashes(command.Password, searchedUser.Password))
            {
                return new GenericCommandResult(false, "Invalid username or password", "");
            }

            if (searchedUser == null)
            {
                return new GenericCommandResult(false, "Invalid username or password", "");
            }

            return new GenericCommandResult(true, "Successfully logged in!", searchedUser);
        }
    }
}
