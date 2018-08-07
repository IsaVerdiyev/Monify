using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services
{

    public enum StringsDictionaryEnum
    {
        AllUsers,
        Balance,
        Day, 
        Week,
        Month, 
        Year,
        All,
        Currencies,
        Accounts, 
        Categories,
        Settings,
        LastUpdateDate,
        Currency,
        CleanData,
        NewAccount,
        ValuteOfAccount,
        InitialAmountOfMoney,
        Add,
        Profit,
        Choose,
        Category,
        Transaction
    }

    class AppStringsDictionaryStorage
    {
        public static List<string> strings;

        public AppStringsDictionaryStorage()
        {
            

            strings = new List<string>
            {
                StringsDictionaryEnum.AllUsers.ToString(),
                StringsDictionaryEnum.Balance.ToString(),
                StringsDictionaryEnum.Day.ToString(),
                StringsDictionaryEnum.Week.ToString(),
                StringsDictionaryEnum.Month.ToString(),
                StringsDictionaryEnum.Year.ToString(),
                StringsDictionaryEnum.All.ToString(),
                StringsDictionaryEnum.Currencies.ToString(),
                StringsDictionaryEnum.Accounts.ToString(),
                StringsDictionaryEnum.Categories.ToString(),
                StringsDictionaryEnum.Settings.ToString(),
                StringsDictionaryEnum.LastUpdateDate.ToString(),
                StringsDictionaryEnum.Currency.ToString(),
                StringsDictionaryEnum.CleanData.ToString(),
                StringsDictionaryEnum.NewAccount.ToString(),
                StringsDictionaryEnum.ValuteOfAccount.ToString(),
                StringsDictionaryEnum.InitialAmountOfMoney.ToString(),
                StringsDictionaryEnum.Add.ToString(),
                StringsDictionaryEnum.Profit.ToString(),
                StringsDictionaryEnum.Choose.ToString(),
                StringsDictionaryEnum.Category.ToString(),
                StringsDictionaryEnum.Transaction.ToString()
            };
        }
        
    }
}
