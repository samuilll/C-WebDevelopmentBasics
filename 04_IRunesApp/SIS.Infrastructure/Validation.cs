﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SIS.Infrastructure
{
    public static class Validation
    {
        public static bool TryValidate(object entity)
        {
            var validationContext = new ValidationContext(entity);

            var results = new List<ValidationResult>();

         return   Validator.TryValidateObject(
                entity,
                validationContext,
                results,
                validateAllProperties: true);
        }

        public static bool IsStringValid(string value, int minLength, int maxLength)
        {
            if (value.Length < minLength || value.Length > maxLength)
            {
                return false;
            }
            return true;
        }
    }
}
