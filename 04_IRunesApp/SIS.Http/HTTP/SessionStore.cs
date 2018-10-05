using System.Collections.Concurrent;

namespace SIS.Http.HTTP
{
    public static class SessionStore
    {
        public  const string SessionCookieKey = "SID";
        public const string CurrentUserKey = "^%Current_User_Session_Key%^";
        public const string ShoppingCartKey = "^%Current_Shopping_Order^%";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions =
            new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
            => sessions.GetOrAdd(id, _ =>new HttpSession(id));
    }
}
