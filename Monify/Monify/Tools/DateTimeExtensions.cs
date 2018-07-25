using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    public static class DateTimeExtensions
    {
        public static DateTime SearchedDayOfWeek(this DateTime datetime, DayOfWeek startOfWeek)
        {
            int diff = (7 + (datetime.DayOfWeek - startOfWeek)) % 7;
            return datetime.AddDays(-1 * diff).Date;
        }

        public static string ToMonthName(this DateTime dateTime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
        }
    }
}
