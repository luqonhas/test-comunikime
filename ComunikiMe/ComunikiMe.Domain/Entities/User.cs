using ComunikiMe.Shared.Entities;
using ComunikiMe.Shared.Enums;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Entities
{
    public class User : Base
    {
        public User(string userName, string email, string password, EnUserType permission)
        {
            AddNotifications(
            new Contract<Notification>()
                .Requires()
                .IsNotEmpty(userName, "UserName", "The 'UserName' field cannot be empty!")
                .IsEmail(email, "Email", "Enter a valid email address")
            );

            if (IsValid)
            {
                UserName = userName;
                Email = email;
                Password = password;
                Permission = permission;
            }
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public EnUserType Permission { get; set; }

        //Compositions
        public IReadOnlyCollection<Cart> Carts { get; private set; }
        private List<Cart> _cart { get; set; }

    }
}
