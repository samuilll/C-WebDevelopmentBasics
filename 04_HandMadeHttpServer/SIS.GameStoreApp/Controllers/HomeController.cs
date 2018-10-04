using System;
using System.Collections.Generic;
using System.Text;
using GamesStoreData.Models.ViewModels;
using GamesStoreData.Services;
using GamesStoreData.Services.Contracts;
using SIS.Http.HTTP;
using SIS.Http.HTTP.Contracts;
using SIS.Http.HTTP.Response;

namespace SIS.GameStoreApp.Controllers
{
   public class HomeController:BaseController
    {
        private IGameService gameService;

        public HomeController()
             : base()
        {
            this.gameService = new GameService(this.mapper);
        }

        public IHttpResponse Index(IHttpRequest req)
        {

            bool isLoggedIn = req.Session.IsAuthenticated();


            bool isAdmin = isLoggedIn
                           ?
                           req.Session.Get<LoginViewModel>(SessionStore.CurrentUserKey).IsAdmin
                           :
                           false;

            List<GameHomeViewModel> games;

            if (req.UrlParameters.Count == 0
                || req.UrlParameters["filter"] == "All")
            {
                games = this.gameService.GetAllGames();
            }
            else
            {
                games = this.gameService.GetOwnedGames(req.Session.Get<LoginViewModel>(SessionStore.CurrentUserKey).Email);
            }

            StringBuilder sb = new StringBuilder();

            CreateHtmlAllGamesString(isLoggedIn,isAdmin, games, sb);

            this.ViewData["games"] = sb.ToString();

            LoginViewModel user = req.Session.Get<LoginViewModel>(SessionStore.CurrentUserKey);

            if (isAdmin)
            {
                return this.FileViewResponse("Home/admin-home");
            }
             if(isLoggedIn)
            {
                return this.FileViewResponse("Home/user-home");
            }
          
                return this.FileViewResponse("Home/guest-home");
            
        }

        private static void CreateHtmlAllGamesString(bool isLoggedIn, bool isAdmin, List<GameHomeViewModel> games,
            StringBuilder sb)
        {
            for (int i = 0; i < games.Count; i++)
            {
                GameHomeViewModel game = games[i];

                bool hasCardGroupOpen = i % 3 == 0;
                bool hasCardGroupClosed = i % 3 == 2;

                if (hasCardGroupOpen)
                {
                    sb.Append("<div class=\"card-group\">");
                }

                sb.Append("<div class=\"card col-4 thumbnail\">" +
                          "<img " +
                          "class=\"card-image-top img-fluid img-thumbnail\"" +
                          $"onerror=\"this.src='https://i.ytimg.com/vi/{game.Trailer}/maxresdefault.jpg';\"" +
                          $"src=\"{game.ThumbnailUrl}\">" +
                          "<div class=\"card-body\">" +
                          $"<h4 class=\"card-title\">{game.Title}</h4>" +
                          $"<p class=\"card-text\"><strong>Price</strong> - {game.Price}&euro;</p>" +
                          $"<p class=\"card-text\"><strong>Size</strong> - {game.Size} GB</p>" +
                          $"<p class=\"card-text\">{game.Description}</p>" +
                          "</div>" +
                          "<div class=\"card-footer\">");
                if (isAdmin)
                {
                    sb.Append(
                        $"<a class=\"card-button btn btn-warning\" name=\"edit\" href=\"edit-game/{game.Id}\">Edit</a>" +
                        $"<a class=\"card-button btn btn-danger\" name=\"delete\" href=\"delete-game/{game.Id}\">Delete</a>");
                }


                if (isLoggedIn)
                {
                    sb.Append(
                        $"<a class=\"card-button btn btn-outline-primary\" name=\"info\" href=\"/game-details/{game.Id}\">Info</a>" +
                        $"<a class=\"card-button btn btn-primary\" name=\"buy\" href=\"/buy-game/{game.Id}\">Buy</a>" +
                        "</div>" +
                        "</div>"
                    );
                }
                else
                {
                    sb.Append(
                        $"<a class=\"card-button btn btn-outline-primary\" name=\"info\" href=\"/game-details/{game.Id}\">Info</a>" +
                        $"<a class=\"card-button btn btn-primary\" name=\"buy\" href=\"/login\">Buy</a>" +
                        "</div>" +
                        "</div>"
                    );
                }

                if (hasCardGroupClosed)
                {
                    sb.Append("</div>");

                }
            }
        }
    }
}
