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

            if (req.FormData.Count==0)
            {
                List<GameHomeViewModel> games = this.gameService.GetAllGames();

                StringBuilder sb = new StringBuilder();

                foreach (GameHomeViewModel game in games)
                {
                    sb.Append("");
                }
            }

            if (!isLoggedIn)
            {
                return this.FileViewResponse("Home/guest-home");
            }
            LoginViewModel user = req.Session.Get<LoginViewModel>(SessionStore.CurrentUserKey);

            if (user.IsAdmin)
            {
                return this.FileViewResponse("Home/admin-home");
            }
            else
            {
                return this.FileViewResponse("Home/user-home");
            }
        }
    }
}
