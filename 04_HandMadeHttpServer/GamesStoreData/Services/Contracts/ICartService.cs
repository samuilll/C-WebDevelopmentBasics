using System;
using System.Collections.Generic;
using System.Text;

namespace GamesStoreData.Services.Contracts
{

   public interface ICartService
   {
       bool BuyGames(int id);

       bool AlreadyHasThisGame(int id);
   }
}
