using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using GamesStoreData.Services.Contracts;

namespace GamesStoreData.Services
{
  public  class CartService:ICartService
    {
        private IMapper mapper;

        public CartService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public bool BuyGames(int id)
        {
            throw new NotImplementedException();
        }

        public bool AlreadyHasThisGame(int id)
        {
            return false;
        }
    }
}
