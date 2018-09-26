using System;
using System.Linq;

namespace Task03_RequestParser
{
    class StartUp
    {
        static void Main(string[] args)
        {
            RequestHolder requestHolder = new RequestHolder();

            Engine engine = new Engine(requestHolder);

            engine.Run();

        }
    }
}
