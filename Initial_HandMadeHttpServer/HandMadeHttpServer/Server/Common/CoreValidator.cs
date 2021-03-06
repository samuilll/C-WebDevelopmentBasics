﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Common
{
   public static class CoreValidator
    {
        public static void ThrowIfNull(object obj, string name)
        {
            if (obj==null)
            {
                throw new ArgumentException(name);
            }
        }

        public static void ThrowIfNullOrEmpty(string text, string name)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentException($"{name} cannot be null or empty");
            }
        }
    }
}
