namespace travelExpense.Utils
{
    public class DateUtils
    {

        public static string FormatDateTime(DateTime dateTime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return dateTime.ToString(format);
        }


    }
}
