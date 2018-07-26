using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Monify.Tools;
using static Monify.Tools.DateTimeExtensions;

namespace Monify.Converters
{
    class DateToStringMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime selectedDate;

            if((DateTime?)values[0] != null)
            {
                selectedDate = (DateTime)values[0];
            }
            else { return null; }
            

            DateInterval dateInterval = (DateInterval)values[1];

            bool isMainDate = (bool)values[2];

            if(dateInterval == DateInterval.Day)
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
            else if (dateInterval == DateInterval.Week)
            {
              
                return $"{selectedDate.SearchedDayOfWeek(DayOfWeek.Monday).Day} - {selectedDate.SearchedDayOfWeek(DayOfWeek.Sunday).Day} {selectedDate.ToMonthName()}";
            }
            else if (dateInterval == DateInterval.Month)
            {
                return $"{selectedDate.ToMonthName()}";
            }
            else if (dateInterval == DateInterval.Year)
            {
                return $"{selectedDate.Year}";
            }
            else if (dateInterval == DateInterval.All)
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
