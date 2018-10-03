﻿using System;
using System.Collections.Generic;
using System.Text;
using GamesStoreData.Models.ViewModels;

namespace GamesStoreData.Services.Contracts
{
    public interface IGameService
    {
        bool CreateGame(GameToAdViewModel gameView);
    }
}