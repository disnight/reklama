using System;

namespace Domain.Utils
{
    public static class Helper
    {
        public static string RemoveTextFromText(string text, string startText, string endText)
        {
            var startIndex = text.IndexOf(startText, StringComparison.Ordinal);
            if (startIndex == -1) return text;
            var endIndex = text.IndexOf(endText, startIndex + 1, StringComparison.Ordinal);
            return text.Replace(text.Substring(startIndex, (endIndex - startIndex) + 1), "");
        }
    }
}