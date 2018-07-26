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
    class OperationToCategoryAmountMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Operation operation = values[0] as Operation;

            IStorage storage = values[1] as IStorage;

            return $"{storage.OperationCategories.FirstOrDefault(cat => cat.Index == operation?.OperationCategoryIndex)} - {operation?.Amount}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
