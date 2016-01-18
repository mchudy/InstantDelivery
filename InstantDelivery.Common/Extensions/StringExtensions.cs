using System.Text;

namespace InstantDelivery.Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Zamienia polskie znaki w danym napisie na odpowiednie znaki z alfabetu łacińskiego
        /// </summary>
        /// <param name="s">Dany napis</param>
        /// <returns>Napis z zamienionymi polskimi znakami</returns>
        public static string ReplaceNationalCharacters(this string s)
        {
            StringBuilder sb = new StringBuilder(s);
            sb.Replace('ą', 'a')
              .Replace('ć', 'c')
              .Replace('ę', 'e')
              .Replace('ł', 'l')
              .Replace('ń', 'n')
              .Replace('ó', 'o')
              .Replace('ś', 's')
              .Replace('ż', 'z')
              .Replace('ź', 'z')
              .Replace('Ą', 'A')
              .Replace('Ć', 'C')
              .Replace('Ę', 'E')
              .Replace('Ł', 'L')
              .Replace('Ń', 'N')
              .Replace('Ó', 'O')
              .Replace('Ś', 'S')
              .Replace('Ż', 'Z')
              .Replace('Ź', 'Z');
            return sb.ToString();
        }
    }
}
