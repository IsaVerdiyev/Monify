using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.Models;
using Monify.Tools;

namespace Monify.Services
{
    static class CurrencyConverter
    {
        static IStorage storage = StorageGetter.Storage;

       

        public static double Convert(int sourceCurrencyIndex, int targetCurrencyIndex, double amount)
        {
            Currency sourceCurrency = storage.Currencies.FirstOrDefault(c => c.Id == sourceCurrencyIndex);

            Currency targetCurrency = storage.Currencies.FirstOrDefault(c => c.Id == targetCurrencyIndex);

            return Convert(sourceCurrency, targetCurrency, amount);
        }

        public static  double Convert(Currency sourceCurrency, Currency targetCurrency, double amount)
        {
            return amount * sourceCurrency.Value / targetCurrency.Value;
        }

    }
}
