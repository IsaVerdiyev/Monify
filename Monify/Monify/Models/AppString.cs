using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    public enum AppStringEnum
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
        Choose,
        Category
    }

    class AppString
    {
        static int iterator = 0;
        int id;
        string word;

        public AppString()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }
        public string Word { get => word; set => word = value; }
    }
}
