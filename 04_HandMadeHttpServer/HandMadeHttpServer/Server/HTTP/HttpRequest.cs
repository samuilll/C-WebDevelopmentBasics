using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.SecurityTokenService;
namespace HandMadeHttpServer.Server.HTTP
{
    using Contracts;
    using HandMadeHttpServer.Server.Common;
    using HandMadeHttpServer.Server.Enums;
    using HandMadeHttpServer.Server.Exceptoins;
    using System.Linq;
    using System.Net;

    public class HttpRequest : IHttpRequest
    {

        private const string BadRequestMessage = "Invalid request line";

        public HttpRequest(string requestString)
        {
            this.FormData = new Dictionary<string, string>();
            this.QueryParameters = new Dictionary<string, string>();
            this.HeaderCollection = new HttpHeaderCollection();
            this.UrlParameters = new Dictionary<string, string>();
            this.Cookies = new HttpCookieCollection();

            this.ParseRequest(requestString);
        }

    
        //GET /users/register HTTP/1.0

        public Dictionary<string, string> FormData { get; }

        public HttpHeaderCollection HeaderCollection { get; }

        public string Path { get; private set; }

        public Dictionary<string, string> QueryParameters { get; }

        public HttpRequestMethod RequestMethod { get; private set; }

        public string Url { get; private set; }

        public Dictionary<string, string> UrlParameters { get; }

        public IHttpCookieCollection Cookies { get; private set; }

        public IHttpSession Session { get;  set; }

        public void AddUrlParameter(string key, string value)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            this.UrlParameters[key] = value;
        }
        private void ParseFormData(string formDataLine)
        {
            if (this.RequestMethod == HttpRequestMethod.GET)
            {
                return;
            }
            this.ParseQuery(formDataLine, this.FormData);
        }

        private void ParseRequest(string requestString)
        {
            string[] requestLines = requestString
                .Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            string[] requestLine = requestLines[0].Trim()
           .Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (requestLine.Length != 3 || requestLine[2].ToLower() != "http/1.1")
            {
                throw new BadRequestException(BadRequestMessage);
            }

            this.RequestMethod = this.ParseRequestMethod(requestLine[0].ToUpper());
            this.Url = requestLine[1];
            this.Path = this.Url
                .Split(new[] { '?', '#' }, StringSplitOptions.RemoveEmptyEntries)[0];

            this.ParseHeaders(requestLines);
            this.ParseCookies();
            this.ParseParameters();
            this.ParseFormData(requestLines.Last());

            this.SetSession();
        }

        private void SetSession()
        {
            var sessionCookieKey = SessionStore.SessionCookieKey;

            if (this.Cookies.ContainsKey(sessionCookieKey))
            {
                var cookie = this.Cookies.GetCookie(sessionCookieKey);
                var sessionId = cookie.Value;

                this.Session = SessionStore.Get(sessionId);
            }
    }

    private void ParseCookies()
        {
            if (this.HeaderCollection.ContainsKey(HttpHeader.Cookie))
            {
                var allCookies = this.HeaderCollection.Get(HttpHeader.Cookie);

                foreach (var cookie in allCookies)
                {
                    if (!cookie.Value.Contains('='))
                    {
                        return;
                    }

                    var cookieParts = cookie
                        .Value
                        .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .ToList();

                    if (!cookieParts.Any())
                    {
                        continue;
                    }

                    foreach (var cookiePart in cookieParts)
                    {
                        var cookieKeyValuePair = cookiePart
                            .Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                        if (cookieKeyValuePair.Length == 2)
                        {
                            var key = cookieKeyValuePair[0].Trim();
                            var value = cookieKeyValuePair[1].Trim();

                            this.Cookies.Add(new HttpCookie(key, value, false));
                        }
                    }
                }
            }
        }
        private void ParseHeaders(string[] requestLines)
        {
            int endIndex = Array.IndexOf(requestLines, string.Empty);

            for (int i = 1; i < endIndex; i++)
            {
                var currentLine = requestLines[i];
                var headerParts = currentLine.Split(new[] { ": " }, StringSplitOptions.RemoveEmptyEntries);

                if (headerParts.Length != 2)
                {
                    throw new BadRequestException(BadRequestMessage);
                }

                var headerKey = headerParts[0];
                var headerValue = headerParts[1].Trim();

                var header = new HttpHeader(headerKey, headerValue);

                this.HeaderCollection.Add(header);
            }

            if (!this.HeaderCollection.ContainsKey(HttpHeader.Host))
            {
                throw new BadRequestException(BadRequestMessage);
            }
        }

        private void ParseParameters()
        {
            if (!this.Url.Contains('?'))
            {
                return;
            }

            var query = this.Url
                .Split(new[] { '?' }, StringSplitOptions.RemoveEmptyEntries)
                .Last();

            ParseQuery(query, this.UrlParameters);
        }

        private void ParseQuery(string query, Dictionary<string, string> dict)
        {

            if (!query.Contains('='))
            {
                return;
            }

            var queryPairs = query.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var queryPair in queryPairs)
            {
                var queryKvp = queryPair.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

                if (queryKvp.Length != 2)
                {
                    return;
                }

                var queryKey = WebUtility.UrlDecode(queryKvp[0]);
                var queryValue = WebUtility.UrlDecode(queryKvp[1]);

                dict.Add(queryKey, queryValue);
            }
        }

        private HttpRequestMethod ParseRequestMethod(string methodAsString)
        {
            bool success = Enum.TryParse<HttpRequestMethod>(methodAsString, out HttpRequestMethod result);

            if (!success)
            {
                throw new InvalidHttpMethodException();
            }

            return result;
        }

    }
}
