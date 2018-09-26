using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Task03_RequestParser.Enums;

namespace Task03_RequestParser
{
   internal class Engine
    {
        private const string END_COMMAND = "END";

        private RequestHolder requestHolder;
        private Writer writer;

        public Engine(RequestHolder requestHolder)
        {
            this.requestHolder = requestHolder;
            this.writer = new Writer();
        }

        public void Run()
        {
            AddPossibleRequests();

            Response response = CreateResponse();

            this.writer.WriteLine(response.ToString());
        }

        private Response CreateResponse()
        {
            string[] requestArgs = Console.ReadLine().Split(' ');

            string method = requestArgs[0];
            string path = requestArgs[1];
            string protocol = requestArgs[2];

            Enum.TryParse<RequestType>(method, out RequestType requestType);

            Request request = new Request(requestType, path);

            string statusText = this.requestHolder.DoesRequestExist(request) == true ? "OK" : "NOTFOUND";

            int statusCode = statusText == "OK" ? 200 : 404;

            Response response = new Response(protocol, statusCode, statusText);
            return response;
        }

        private void AddPossibleRequests()
        {
            string requestString = Console.ReadLine();

            while (!string.Equals(requestString, END_COMMAND))
            {
                string[] requstArgs = requestString.Split('/');

                string method = requstArgs.Last().ToUpper();

                string path = string.Join("/", requstArgs.Take(requstArgs.Length - 1));

                Enum.TryParse<RequestType>(method,out RequestType requestType);

                Request request = new Request(requestType,path);

                requestHolder.AddRequest(request);

                requestString = Console.ReadLine();
            }
        }
    }
}
