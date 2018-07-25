using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Monify.Tools;

namespace Monify.Converters
{
    class DateToStringMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime selectedDate = (DateTime)values[0];

            TimeSpan duration = (TimeSpan)values[1];

            bool isMainDate = (bool)values[2];

            if(duration == TimeSpan.FromDays(1))
            {
                if (isMainDate)
                {
                    return $"{selectedDate.DayOfWeek}, {selectedDate.Day} {selectedDate.ToMonthName()}";
                }
                else
                {
                    return $"{selectedDate.Day} {selectedDate.ToMonthName()}";
                }
            }
            else if (duration == TimeSpan.FromDays(7))
            {
                return $"{selectedDate.SearchedDayOfWeek(DayOfWeek.Monday).Day} - {selectedDate.SearchedDayOfWeek(DayOfWeek.Sunday).Day} {selectedDate.Month}";
            }
            else if (duration == TimeSpan.FromDays(30))
            {
                return $"{selectedDate.Month}";
            }
            else if (duration == TimeSpan.FromDays(355))
            {
                return $"{selectedDate.Year}";
            }
            else if (duration == TimeSpan.MaxValue)
            {
                if (isMainDate)
                {
                    return "All";
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
