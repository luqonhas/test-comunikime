using ComunikiMe.Domain.Entities;
using ComunikiMe.Domain.Interfaces;
using ComunikiMe.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComunikiMe.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ComunikiMeContext _ctx;

        public UserRepository(ComunikiMeContext ctx)
        {
            _ctx = ctx;
        }



        // Commands:
        public void Add(User user)
        {
            _ctx.Users.Add(user);
            _ctx.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _ctx.Users.Remove(SearchById(id));
            _ctx.SaveChanges();
        }



        // Queries:
        public IEnumerable<User> List()
        {
            return _ctx.Users
                .AsNoTracking()
                .ToList();
        }

        public User SearchById(Guid id)
        {
            return _ctx.Users.FirstOrDefault(x => x.Id == id);
        }

        public User SearchByEmail(string email)
        {
            return _ctx.Users.FirstOrDefault(x => x.Email == email);
        }
        public User SearchByUserName(string userName)
        {
            return _ctx.Users.FirstOrDefault(x => x.UserName == userName);
        }
    }
}
