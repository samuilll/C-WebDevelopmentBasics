﻿using System;
using SIS.Http.Common;

namespace SIS.Http.HTTP
{
    public class HttpCookie
    {
        public HttpCookie(string key, string value, int expires=3)
        {
            CoreValidator.ThrowIfNullOrEmpty(key, nameof(key));
            CoreValidator.ThrowIfNullOrEmpty(value, nameof(value));

            Key = key;
            Value = value;
            this.Expires = DateTime.UtcNow.AddDays(expires);
        }

        public HttpCookie(string key, string value, bool isNew, int expires=3)
            :this(key,value,expires)
        {
            this.IsNew = isNew;
        }

        public string Key { get; private set; }

        public string Value { get; private set; }

        public DateTime Expires { get; private set; }

        public bool IsNew { get; private set; } = true;

        public override string ToString()
        {
            return $"{this.Key}={this.Value}; Expires = {this.Expires.ToLongTimeString()}";
        }


    }
}
