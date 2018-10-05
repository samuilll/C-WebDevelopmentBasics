using System;
using System.Linq;
using IRunes.Domain.Models;
using IRunes.Domain.ViewModels;
using IRunesData;
using IRunesServices.Contracts;
using SIS.Infrastructure;

namespace IRunesServices
{
    public class UserService:IUserService
    {
        private IHashService hashService;

        public UserService()
        {
            this.hashService = new HashService();
        }
        public bool CreateUser(RegisterViewModel registerModel)
        {
            User user = new User()
            {
                Username = registerModel.Username,
                Id = Guid.NewGuid().ToString(),
                HashedPassword = this.hashService.StrongHash(registerModel.Password),
                Email = registerModel.Email
            };

            using (RunesDbContext db = new RunesDbContext())
            {
                if (db.Users.Any(u=>u.Username==user.Username 
                                    || u.Email==user.Email))
                {
                    return false;
                }

                try
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    return true;
                }

                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public string GetByMailOrPass(string emailOrUsername, string password)
        {
            using (RunesDbContext db = new RunesDbContext())
            {
                User user = db
                    .Users
                    .SingleOrDefault(u => (u.Username == emailOrUsername
                                          || u.Email == emailOrUsername));

                if (user==null)
                {
                    return null;
                }

                string hashedPassword = this.hashService.StrongHash(password);

                if (user.HashedPassword!=hashedPassword)
                {
                    return null;
                }

                return user.Username;
            }
        }
    }
}

