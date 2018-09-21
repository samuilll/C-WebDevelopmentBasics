﻿using HandMadeHttpServer.Application.Views;
using HandMadeHttpServer.Server.Enums;
using HandMadeHttpServer.Server.HTTP.Contracts;
using HandMadeHttpServer.Server.HTTP.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Application.Controllers
{
  public  class HomeController
    {
        public IHttpResponse Index()
        {
          return  new ViewResponse(HttpStatusCode.OK,new HomeIndexView());
        }

        public IHttpResponse SessionTest(IHttpRequest req)
        {
            var session = req.Session;

            const string sessionDateKey = "saved_date";

            if (session.Get(sessionDateKey) == null)
            {
                session.Add(sessionDateKey, DateTime.UtcNow);
            }

            return new ViewResponse(
                HttpStatusCode.OK,
                new SessionTestView(session.Get<DateTime>(sessionDateKey)));
        }
    }
}
