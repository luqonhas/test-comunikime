using ComunikiMe.Shared.Commands;
using ComunikiMe.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Commands.Users
{
    public class CreateUserCommand : Notifiable<Notification>, ICommand
    {
        public CreateUserCommand() { }

        public CreateUserCommand(string userName, string email, string password, EnUserType permission)
        {
            UserName = userName;
            Email = email;
            Password = password;
            Permission = permission;
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnUserType Permission { get; set; }

        public void Validate()
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(UserName, "UserName", "The 'UserName' field cannot be empty!")
                .IsEmail(Email, "Email", "Enter a valid email address")
                .IsNotEmpty(Password, "Password", "The 'Password' field cannot be empty!")
            );
        }
    }
}
