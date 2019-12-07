using System;

namespace Admin.Helpers
{
    public static class TimeConterter
    {
        public static TimeConverterResult ConvertFromMinutes(int minutes)
        {
            var Minutes = minutes % 60;
            var Hours = minutes / 60;
            var Days = Hours / 24;
            Hours = Hours % 24;
            TimeConverterResult result = new TimeConverterResult(Days, Hours, Minutes, 0);
            return result;
        }
    }
}