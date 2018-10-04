using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GamesStoreData.Models.ViewModels
{
   public class CartViewModel
   {
       private List<GameInCartViewModel> games;

       private IMapper mapper;

       public decimal TotalPrice => this.games.Sum(g=>g.Price);

       public CartViewModel(IMapper autoMapper)
       {
           this.mapper = autoMapper;
           this.games = new List<GameInCartViewModel>();
       }

       public override string ToString()
       {
           return base.ToString();
       }

       public List<int> GetGamesIds()
       {
           return this.games.Select(g => g.Id).ToList();
       }

       public void AddGame(int id)
       {
           using (GameStoreDbContext db = new GameStoreDbContext())
           {
               Game game = db
                   .Games
                   .SingleOrDefault(g => g.Id == id);

               GameInCartViewModel gameForCart = this.mapper.Map<GameInCartViewModel>(game);

               this.games.Add(gameForCart);
           }
       }
   }
}
