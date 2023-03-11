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
    public class LoginEmailHandle : Notifiable<Notification>, IHandlerCommand<LoginEmailCommand>
    {
        private readonly IUserRepository _userRepository;

        public LoginEmailHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handler(LoginEmailCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Enter the data correctly", command.Notifications);
            }

            var searchedUser = _userRepository.SearchByEmail(command.Email);

            if (!Password.ValidateHashes(command.Password, searchedUser.Password))
            {
                return new GenericCommandResult(false, "Invalid e-mail or password", "");
            }

            if (searchedUser == null)
            {
                return new GenericCommandResult(false, "Invalid e-mail or password", "");
            }

            return new GenericCommandResult(true, "Successfully logged in!", searchedUser);
        }
    }
}