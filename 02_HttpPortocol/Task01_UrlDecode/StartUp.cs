using System;
using System.Net;

namespace ConsoleApp1
{
    class StartUp
    {
        static void Main(string[] args)
        {

            string urlToDecode = Console.ReadLine();

           string decodedUrl = WebUtility.UrlDecode(urlToDecode);

            Console.WriteLine(decodedUrl);

        }
    }
}
