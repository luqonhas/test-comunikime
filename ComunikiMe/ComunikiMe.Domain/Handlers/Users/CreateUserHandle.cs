using ComunikiMe.Domain.Commands.Users;
using ComunikiMe.Domain.Entities;
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

namespace ComunikiMe.Domain.Handlers.Users
{
    public class CreateUserHandle : Notifiable<Notification>, IHandlerCommand<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handler(CreateUserCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly enter user data", command.Notifications);
            }

            var emailExists = _userRepository.SearchByEmail(command.Email);

            if (emailExists != null)
            {
                return new GenericCommandResult(false, "Existing e-mail", "Enter another e-mail");
            }

            command.Password = Password.Encrypt(command.Password);

            //User newUser = new User(command.UserName, command.Email, command.Password, command.Permission);
            User newUser = new User(command.UserName, command.Email, command.Password, command.Permission);

            if (!newUser.IsValid)
            {
                return new GenericCommandResult(false, "Invalid user data", newUser.Notifications);
            }

            _userRepository.Add(newUser);

            return new GenericCommandResult(true, "Users created successfully!", newUser);
        }
    }
}
