using System;
using System.Collections.Generic;
using System.Text;
using SIS.Http.HTTP.Contracts;

namespace IRunesApp.Controllers
{
  public  class TrackController:BaseController
    {
        public IHttpResponse Create(Dictionary<string, string> urlParameters)
        {
            string albumId = urlParameters["albumId"];

            return null;
        }

        internal IHttpResponse Create(IHttpRequest req)
        {
            throw new NotImplementedException();
        }

        public IHttpResponse Details(IHttpRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
