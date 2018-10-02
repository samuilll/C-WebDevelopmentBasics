namespace GamesStoreData.Common
{
    public static class ExtensionMethods
    {
        public static bool HasLowerCase(this string value)
        {
            bool result = false;

            foreach (char letter in value.ToCharArray())
            {
                if (char.IsLower(letter))
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static bool HasUpperCase(this string value)
        {
            bool result = false;

            foreach (char letter in value.ToCharArray())
            {
                if (char.IsUpper(letter))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static bool HasDigit(this string value)
        {
            bool result = false;

            foreach (char letter in value.ToCharArray())
            {
                if (char.IsDigit(letter))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
