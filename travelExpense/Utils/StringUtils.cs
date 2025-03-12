namespace travelExpense.Utils
{
    public class StringUtils
    {
        public StringUtils() { }

        public static string Truncate(string str, int length)
        {
            if (string.IsNullOrEmpty(str) || length <= 0) return string.Empty;
            if (str.Length <= length) return str;
            return str.Substring(0, length);
        }

    }
}
