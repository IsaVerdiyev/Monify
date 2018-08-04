using Monify.Models;
using Monify.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Monify.Converters
{
    class OperationCategoryTypeIndexToColorMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Operation operation = values[0] as Operation;

            IStorage storage = values[1] as IStorage;

            if (storage.OperationCategories.FirstOrDefault(o => o.Id == operation.OperationCategoryIndex).OperationTypeIndex == storage.OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id)
            {
                return new SolidColorBrush(Colors.Green);
            }
            else if (storage.OperationCategories.FirstOrDefault(o => o.Id == operation.OperationCategoryIndex).OperationTypeIndex == storage.OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
