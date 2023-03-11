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

namespace ComunikiMe.Domain.Handlers.Users
{
    public class DeleteUserHandle : Notifiable<Notification>, IHandlerCommand<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserHandle(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICommandResult Handler(DeleteUserCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new GenericCommandResult(false, "Correctly inform the user you want to delete", command.Notifications);
            }

            var searchedUser = _userRepository.SearchById(command.Id);

            if (searchedUser == null)
            {
                return new GenericCommandResult(false, "User not found", command.Notifications);
            }

            _userRepository.Delete(searchedUser.Id);

            return new GenericCommandResult(false, "User deleted successfully!", "");
        }
    }
}
