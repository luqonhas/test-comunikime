using ComunikiMe.Shared.Enums;
using ComunikiMe.Shared.Queries;
using Flunt.Notifications;
using Flunt.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Queries.Users
{
    public class SearchUserByEmailQuery : Notifiable<Notification>, IQuery
    {
        public string Email { get; set; }

        public void Validate()
        {
            AddNotifications(
                new Contract<Notification>()
                    .Requires()
                    .IsEmail(Email, "email", "Enter a valid email address")
                );
        }

        public class SearchByEmailResult
        {
            public Guid Id { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public EnUserType UserType { get; set; }
            public DateTime CreationDate { get; set; }
        }
    }
}
