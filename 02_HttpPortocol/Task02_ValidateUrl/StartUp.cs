using System;
using System.Net;

namespace Task02_ValidateUrl
{
    class StartUp
    {
        static void Main(string[] args)
        {
            string urlToValidate = Console.ReadLine();

            bool isValidUrl = Uri.TryCreate(urlToValidate, UriKind.Absolute, out Uri result) &&
             ((result.Scheme == Uri.UriSchemeHttp && result.Port==80) ||
              (result.Scheme == Uri.UriSchemeHttps) && result.Port==443);

            if (!isValidUrl)
            {
                Console.WriteLine("InvalidUrl");

                return;
            }
            Console.WriteLine($"Protocol: {result.Scheme}");
            Console.WriteLine($"Host: {result.Host}");
            Console.WriteLine($"Port: {result.Port}");
            Console.WriteLine($"Path: {result.LocalPath}");

            if (!string.IsNullOrEmpty(result.Query))
            {
                Console.WriteLine($"Query: {result.Query.Substring(1)}");
            }
            if (!string.IsNullOrEmpty(result.Fragment))
            {
                Console.WriteLine($"Fragment: {result.Fragment.Substring(1)}");
            }

        }
    }
}