using System.Text.RegularExpressions;

namespace SorteOnlineDesafio.Application.Commom
{
    public class Util
    {
        public static bool ValidateEmail(string email)
        {
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            bool isValid = Regex.IsMatch(email, emailRegex);

            return isValid;
        }
    }
}
