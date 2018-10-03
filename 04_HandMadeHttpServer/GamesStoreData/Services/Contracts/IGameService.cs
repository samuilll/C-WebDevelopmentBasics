using System;
using System.Collections.Generic;
using System.Text;
using GamesStoreData.Models.ViewModels;

namespace GamesStoreData.Services.Contracts
{
    public interface IGameService
    {
        bool CreateGame(GameToAddViewModel gameView);

        List<GameViewModel> GetAdminGames();

        List<GameHomeViewModel> GetAllGames();

        List<GameHomeViewModel> GetOwnedGames(LoginViewModel loginViewModel);
    }
}
