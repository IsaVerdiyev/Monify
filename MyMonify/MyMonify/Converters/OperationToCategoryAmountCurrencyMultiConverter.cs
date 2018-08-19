using MyMonify.Models;
using MyMonify.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MyMonify.Converters
{
    class OperationToCategoryAmountCurrencyMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Operation operation = values[0] as Operation;

            IStorage storage = values[1] as IStorage;
            if (operation != null)
            {

                return $"{storage.OperationCategories.FirstOrDefault(cat => cat.Id == operation?.OperationCategoryIndex)} - " +
                    $"{operation?.Amount} " +
                    $"{storage.Operations.Join(storage.Accounts, o => o.AccountIndex, a => a.Id, (o, a) => new { Op = o, Ac = a }).Join(storage.Currencies, OpAc => OpAc.Ac.CurrencyIndex, c => c.Id, (OpAc, c) => new {Op = OpAc.Op, Code = c.Code }).FirstOrDefault(item => item.Op == operation).Code } ";
            }
            else return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
