using System;
using System.ComponentModel.DataAnnotations;

namespace SIS.HTTP.Common
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

        public static void ValidateObject(object entity)
        {
            var validationContext = new ValidationContext(entity);

            Validator.ValidateObject(
                entity,
                validationContext,
                validateAllProperties: true);
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

