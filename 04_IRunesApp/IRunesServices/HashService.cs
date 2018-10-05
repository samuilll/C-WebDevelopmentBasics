using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using IRunesServices.Contracts;

namespace IRunesServices
{
   public class HashService:IHashService
    {
        public string StrongHash(string password)
        {
            string result = password;

            for (int i = 0; i < 10; i++)
            {
                result = Hash(result);
            }

            return result;
        }

        private string Hash(string text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;     
        }
    }
}
