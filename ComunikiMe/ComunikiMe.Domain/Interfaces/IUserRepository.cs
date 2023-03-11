using ComunikiMe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Domain.Interfaces
{
    public interface IUserRepository
    {
        // Commands:
        void Add(User user);

        void Delete(Guid id);



        // Queries:
        IEnumerable<User> List();

        User SearchById(Guid id);

        User SearchByEmail(string email);
        User SearchByUserName(string userName);
    }
}
