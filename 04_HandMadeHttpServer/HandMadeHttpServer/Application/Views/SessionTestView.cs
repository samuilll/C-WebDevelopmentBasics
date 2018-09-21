using HandMadeHttpServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Application.Views
{
  public  class SessionTestView:IView
    {
        private readonly DateTime dateTime;

        public SessionTestView(DateTime dateTime)
        {
            this.dateTime = dateTime;
        }

        public string View()
        {
            return $"<h1>Saved date: {dateTime}</h1>";
        }
    }
}
