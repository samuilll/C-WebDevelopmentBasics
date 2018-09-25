﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HandMadeHttpServer.Server.Enums
{
    public enum HttpStatusCode
    {
        OK = 200,
        MovedPermanently = 301,
        Found = 302,
        MovedTemporarily = 303,
        BadRequest=400,
        NotAuthorized = 401,
        NotFound = 404,
        InternalServerError = 500
    }
}
