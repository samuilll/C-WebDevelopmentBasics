using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task03_RequestParser
{
   internal class RequestHolder
    {

        private IList<Request> requests;

        public RequestHolder()
        {
            this.requests = new List<Request>();

        }

        public IReadOnlyList<Request> Requests
        {
           get
            {
                return (IReadOnlyList<Request>)this.requests;
            }        
        }

        internal void AddRequest(Request request)
        {
            this.requests.Add(request);
        }

   
        internal bool DoesRequestExist(Request request)
        {
            return this.requests.Any(req => req.CompareTo(request)==0);
          //  return this.requests.Contains(request,IEqualityComparer<Request>));
        }
    }
}
