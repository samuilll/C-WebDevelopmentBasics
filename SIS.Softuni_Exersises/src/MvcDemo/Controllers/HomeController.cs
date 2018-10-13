using System;
using System.Collections.Generic;
using System.Text;
using SIS.HTTP.Responses;
using SIS.MvcFramework.ActionResults.Contracts;
using SIS.MvcFramework.Controllers;

namespace MvcDemo.Controllers
{
  public  class HomeController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
