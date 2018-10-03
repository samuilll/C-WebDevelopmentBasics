using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GamesStoreData.Models.ViewModels;
using GamesStoreData.Services.Contracts;
using Microsoft.EntityFrameworkCore.Internal;
using SIS.Infrastructure;
using System.Linq;
using GamesStoreData.Models;

namespace GamesStoreData.Services
{
   public class UserService:IUserService
   {
       private IMapper mapper;

        public UserService(IMapper autoMapper)
        {
            this.mapper = autoMapper;
        }
        public bool CreateUser(RegisterViewModel registerModel)
        {
            bool isUserValid = Validation.TryValidate(registerModel);

            if (!isUserValid)
            {
                return false;
            }

            using (GameStoreDbContext db = new GameStoreDbContext())
            {
                if (db.Users.Any(u=>u.Email==registerModel.Email))
                {
                    return false;
                }

                User user = this.mapper.Map<User>(registerModel);

                if (!db.Users.Any())
                {
                    user.IsAdmin = true;
                }

                db.Users.Add(user);
                db.SaveChanges();

                return true;
            }
        }

       public LoginViewModel GetByMailAndPass(string email, string password)
       {
           using (GameStoreDbContext db = new GameStoreDbContext())
           {
               User user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

               if (user==null)
               {
                   return null;
               }

               return this.mapper.Map<LoginViewModel>(user);
            }
        }
   }
}
