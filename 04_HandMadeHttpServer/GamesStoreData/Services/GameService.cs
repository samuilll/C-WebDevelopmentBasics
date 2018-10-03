using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        public bool CreateGame(GameToAddViewModel gameView)
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

        public List<GameViewModel> GetAdminGames()
        {
            using (GameStoreDbContext db = new GameStoreDbContext())
            {
                List<GameViewModel> games = db
                                            .Games
                                            .ProjectTo<GameViewModel>(mapper.ConfigurationProvider)
                                            .ToList();

                for (int i = 0; i < games.Count; i++)
                {
                    games[i].SequelNumber = i + 1;
                }

                return games;
            }
        }

        public List<GameHomeViewModel> GetAllGames()
        {
            using (GameStoreDbContext db = new GameStoreDbContext())
            {
                List<GameHomeViewModel> games = db
                                            .Games
                                            .ProjectTo<GameHomeViewModel>(mapper.ConfigurationProvider)
                                            .ToList();
                return games;
            }
        }
    }
}
