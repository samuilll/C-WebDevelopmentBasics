﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.HTTP.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        object Get(string key);

        void Add(string key, string value);

        void Clear();

        bool IsAuthenticated();
    }
}
