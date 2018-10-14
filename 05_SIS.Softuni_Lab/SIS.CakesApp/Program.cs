using Microsoft.EntityFrameworkCore;
using SIS.HTTP.Enums;
using SIS.WebServer;
using SIS.WebServer.Routing;
using System;
using SIS.CakesApp.Controllers;
using SIS.CakesApp.Data;
using SIS.MvcFramework;

namespace SIS.CakesApp
{
   public class Program
    {
      public  static void Main(string[] args)
        {
            WebHost.Start(new StartUp());
        }
    }
}
