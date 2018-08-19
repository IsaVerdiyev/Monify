using MyMonify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services
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
        public static Dictionary<string,string> strings;

        public AppStringsDictionaryStorage()
        {
            List<string> list = Enum.GetValues(typeof(StringsDictionaryEnum)).Cast<string>().ToList();
            list.AddRange(Enum.GetValues(typeof(OperationCategoryEnum)).Cast<string>());
            strings = list.ToDictionary(t => t, t => t);
        }
        
    }
}
