using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP
{
    using System.Collections.Concurrent;

   public static class SessionStore
    {
        public  const string sessionCookieKey = "SID";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions =
            new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
            => sessions.GetOrAdd(id, _ =>new HttpSession(id));
    }
}
