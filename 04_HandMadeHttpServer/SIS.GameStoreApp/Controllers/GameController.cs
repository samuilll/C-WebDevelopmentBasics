using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using GamesStoreData.Models.ViewModels;
using GamesStoreData.Services;
using GamesStoreData.Services.Contracts;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

namespace SIS.GameStoreApp.Controllers
{
   public class GameController:BaseController
   {
       private IGameService gameService;

       public GameController()
       {
           this.gameService = new GameService(this.mapper);
       }
        public IHttpResponse AllGames(IHttpSession session)
        {
            if (!session.IsAuthenticated())
            {
                return new RedirectResponse("/login");
            }

            LoginViewModel user = session.Get<LoginViewModel>(SessionStore.CurrentUserKey);

            if (!user.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            return this.FileViewResponse("Game/admin-games");
        }

        public IHttpResponse AddGame(IHttpSession session)
        {
            if (!session.IsAuthenticated())
            {
                return new RedirectResponse("/login");
            }

            LoginViewModel user = session.Get<LoginViewModel>(SessionStore.CurrentUserKey);

            if (!user.IsAdmin)
            {
                return new RedirectResponse("/");
            }

            return this.FileViewResponse("Game/add-game");
        }

        public IHttpResponse AddGame(IHttpRequest req)
        {
            string title = req.FormData["title"];
            string trailer = req.FormData["trailer"];
            string thumbnail = req.FormData["thumbnail"];
            string description = req.FormData["description"];
            decimal size = decimal.Parse(req.FormData["size"]);
            DateTime date = DateTime.ParseExact(req.FormData["date"], "MM/dd/yy", CultureInfo.InvariantCulture);
            decimal Price = decimal.Parse(req.FormData["price"]);

            GameToAdViewModel gameModel = new GameToAdViewModel()
            {
                Title =title,
            };
           // bool success = this.gameService.
            return null;
        }
    }
}
