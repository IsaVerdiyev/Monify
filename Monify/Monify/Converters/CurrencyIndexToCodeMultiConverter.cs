using Monify.Models;
using Monify.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Monify.Converters
{
    class CurrencyIndexToCodeMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Account account = values[0] as Account;

            IStorage storage = values[1] as IStorage;

            return storage.Currencies.FirstOrDefault(c => c.Id == account?.CurrencyIndex)?.Code;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
