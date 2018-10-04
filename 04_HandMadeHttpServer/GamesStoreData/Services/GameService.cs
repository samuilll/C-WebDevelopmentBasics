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
        public bool CreateGame(GameToAddOrEditViewModel gameView)
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

        public List<GameHomeViewModel> GetOwnedGames(string email)
        {
            using (GameStoreDbContext db = new GameStoreDbContext())
            {

                User user = db
                    .Users
                    .Include(u => u.Orders)
                    .ThenInclude(o => o.Games)
                    .ThenInclude(og => og.Game)
                    .Where(u => u.Email == email)
                    .FirstOrDefault();

                ICollection<Order> orders = user.Orders;

                List<GameHomeViewModel> games = new List<GameHomeViewModel>();

                foreach (Order order in orders)
                {
                    List<Game> gamesToAdd = order
                        .Games
                        .Select(og => og.Game)
                        .ToList();

                    foreach (Game gameToAdd in gamesToAdd)
                    {
                        GameHomeViewModel gameModel = mapper.Map<GameHomeViewModel>(gameToAdd);

                        games.Add(gameModel);
                    }
                }
                
              return  games;
            }
        }

        public GameToAddOrEditViewModel GetById(int id)
        {
            using (GameStoreDbContext db = new GameStoreDbContext())
            {
                Game game = db
                    .Games
                    .FirstOrDefault(g => g.Id == id);

                if (game==null)
                {
                    return null;        
                }

                GameToAddOrEditViewModel gameModel = this.mapper.Map<GameToAddOrEditViewModel>(game);

                return gameModel;
            }
        }

        public void EditGame(GameToAddOrEditViewModel gameModel)
        {
            if (!Validation.TryValidate(gameModel))
            {
                return;
            }

            using (GameStoreDbContext db =new GameStoreDbContext())
            {
                Game gameToEdit = db
                    .Games
                    .Single(g => g.Id == gameModel.Id);

                gameToEdit.Price = gameModel.Price;
                gameToEdit.Description = gameModel.Description;
                gameToEdit.ThumbnailUrl = gameModel.ThumbnailUrl;
                gameToEdit.Size = gameModel.Size;
                gameToEdit.Trailer = gameModel.Trailer;
                gameToEdit.ReleaseDate = gameModel.ReleaseDate;
                gameToEdit.Title = gameModel.Title;

                db.Games.Update(gameToEdit);

                db.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            using (GameStoreDbContext db = new GameStoreDbContext())
            {
                Game game = db
                    .Games.SingleOrDefault(g => g.Id == id);

                if (game==null)
                {
                    return;
                }

                db.Games.Remove(game);

                db.SaveChanges();
            }
        }

        public bool GameIsOwnedByYou(int id, string email)
        {
            return this.GetOwnedGames(email).Any(g => g.Id == id);
        }
    }
}
