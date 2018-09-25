using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP
{
    using System.Collections.Concurrent;

   public static class SessionStore
    {
        public  const string SessionCookieKey = "SID";
        public const string CurrentUserKey = "^%Current_User_Session_Key%^";
        public const string ShoppingCardKey = "^%Current_Shopping_Cart^%";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions =
            new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
            => sessions.GetOrAdd(id, _ =>new HttpSession(id));
    }
}
