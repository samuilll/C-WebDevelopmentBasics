using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GamesStoreData.Models;
using GamesStoreData.Models.ViewModels;
using GamesStoreData.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using SIS.Infrastructure;

namespace GamesStoreData.Services
{

   public class GameService:IGameService
    {
        private IMapper mapper;

        public GameService(IMapper autoMapper)
        {
            this.mapper = autoMapper;
        }
        public bool CreateGame(GameToAdViewModel gameView)
        {

            bool success = Validation.TryValidate(gameView);

            if (!success)
            {
                return false;
            }

            using (GameStoreDbContext db = new GameStoreDbContext())
            {
                Game game = this.mapper.Map<Game>(gameView);

                db.Games.Add(game);
                db.SaveChanges();

                return true;
            }
        }
    }
}
