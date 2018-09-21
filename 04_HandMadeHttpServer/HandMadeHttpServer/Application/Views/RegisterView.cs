using HandMadeHttpServer.Server.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Application.Views
{
    public class RegisterView : IView
    {
        public string View()
        {

            return
                "<body>" +
                "   <form method=\"POST\">" +
                "      Name</br>" +
                "      <input type=\"text\" name=\"first-name\" placeholder=\"Enter first-name [a-z]\" /><br/>" +
                 "      <input type=\"text\" name=\"middle-name\" placeholder=\"Enter middle-name [a-z]\" /><br/>" +
                 "      <input type=\"text\" name=\"last-name\" placeholder=\"Enter last-name [a-z]\" /><br/>" +
                "      <input type=\"submit\" />" +
                "   </form>" +
                "</body>";
        }
    }
}
