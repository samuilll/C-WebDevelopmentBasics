using System;
using System.Collections.Generic;
using System.Text;
using GamesStoreData.Models.ViewModels;
using GamesStoreData.Services;
using GamesStoreData.Services.Contracts;
using SIS.GameStoreApp.Common;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

namespace SIS.GameStoreApp.Controllers
{
   public class CartController:BaseController
   {
       private ICartService cartService;
       private IGameService gameService;

        public CartController()
        {
            this.cartService = new CartService(this.mapper);
            this.gameService = new GameService(this.mapper);
        }

        public IHttpResponse Buy(IHttpRequest req)
        {
            int gameToBuyId = int.Parse(req.UrlParameters["id"]);

            if (!req.Session.IsAuthenticated())
            {
                return new RedirectResponse("/login");
            }

            string userEmail = req.Session.Get<LoginViewModel>(SessionStore.CurrentUserKey).Email;

            if (!req.Session.Contains(SessionStore.ShoppingCartKey))
            {
                req.Session.Add(SessionStore.ShoppingCartKey,new CartViewModel(this.mapper));
            }

            CartViewModel cart = req.Session.Get<CartViewModel>(SessionStore.ShoppingCartKey);

            List<int> gamesIds = cart.GetGamesIds();

            if (gamesIds.Contains(gameToBuyId)
                || this.gameService.GameIsOwnedByYou(gameToBuyId,userEmail))
            {
                this.ViewData["show-error"] = AppConstants.PossesedGame;
                
                return new RedirectResponse("/");
            }


            cart.AddGame(gameToBuyId);

            this.ViewData["buy-success"] = AppConstants.SuccessfullOrder;

            return new RedirectResponse("/");
        }

        public IHttpResponse ShowGames(IHttpRequest req)
        {
            return new RedirectResponse("/");
        }
    }
}
