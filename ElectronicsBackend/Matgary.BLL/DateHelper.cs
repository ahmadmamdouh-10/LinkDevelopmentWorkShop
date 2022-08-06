

using System;

namespace Matgary.BLL
{
    public class DateHelper
    {
        public static DateTime GetDateTime(long date, int time = 0, int timezone = 0)
        {
            return DateTimeOffset.FromUnixTimeSeconds(date + time * 60 - timezone * 60 * 60).DateTime;
        }
       
        public static long GetTimestamp()
        {
            return (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        }

        public static long DateTimeToTimestamp(DateTime date)
        {
           return new DateTimeOffset(date).ToUnixTimeSeconds();
        }

        public static int DateTimeToTimeOfDay(DateTime date)
        {
            return new DateTimeOffset(date).TimeOfDay.Minutes + new DateTimeOffset(date).TimeOfDay.Hours*60;
        }

        public static DateTime TimeStampToDate(long date, int time, int timezone)
        {
            return DateTimeOffset.FromUnixTimeSeconds(date + time * 60 + timezone * 60 * 60).DateTime;
        }
    }
}
