using System;
using System.Collections.Generic;
using System.Globalization;
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
   public class GameController:BaseController
   {
       private IGameService gameService;

       public GameController()
            :base()
       {
           this.gameService = new GameService(this.mapper);
       }

        public IHttpResponse AdminGames(IHttpSession session)
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

            List<GameViewModel> games = this.gameService.GetAdminGames();

            StringBuilder sb = new StringBuilder();

            foreach (GameViewModel game in games)
            {
                sb.Append(@"<tr class=""table-warning"">" +
                   $@"<th scope=""row"">{game.SequelNumber}</th>" +
                   $"<td>{game.Title}</td>" +
                   $" <td>{game.Size:f1} GB</td>" +
                    $"<td>{game.Price:f1} &euro;</td>" +
                      " <td>" +
                           @"<a href=""#"" class=""btn btn-warning btn-sm"">Edit</a>" +
                      @"<a href=""#"" class=""btn btn-danger btn-sm"">Delete</a>" +
                   "</td>" +
               "</tr>");
            }

            string result = sb.ToString();

            this.ViewData["show-games"] = "block";
            this.ViewData["games"] = result;

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
            DateTime date = DateTime.ParseExact(req.FormData["date"], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            decimal price = decimal.Parse(req.FormData["price"]);

            GameToAddViewModel gameModel = new GameToAddViewModel()
            {
                Title =title,
                Trailer=trailer,
                ThumbnailUrl = thumbnail,
                Description = description,
                Size=size,
                ReleaseDate = date,
                Price=price
            };

            bool success = this.gameService.CreateGame(gameModel);

            if (!success)
            {
                this.ViewData["show-error"] = "block";
                this.ViewData["error"] = AppConstants.IncorrectGameInput;

                return  this.FileViewResponse("Game/add-game");
            }

            this.ViewData["show-game-added"] = "block";
            this.ViewData["game-added"] = AppConstants.GameSuccesfullAdded;

            return this.FileViewResponse("Game/add-game");
        }
    }
}
