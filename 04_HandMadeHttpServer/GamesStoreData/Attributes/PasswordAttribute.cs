using System.ComponentModel.DataAnnotations;
using GamesStoreData.Common;

namespace GamesStoreData.Attributes
{
    public class PasswordAttribute : ValidationAttribute
    {
        public PasswordAttribute()
            : base("Your password is too simple")
        {

        }

        public override bool IsValid(object value)
        {
            string password = (string)value;

            bool result = password.Length >= 6
                          && password.HasDigit()
                          && password.HasLowerCase()
                          && password.HasUpperCase();

            return result;
        }
    }
}
