using Monify.Models;
using Monify.Services.CurrencyGetterService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services
{
    interface IStorage
    {
        ObservableCollection<Account> Accounts { get; set; }
        ObservableCollection<OperationType> OperationTypes { get; set; }
        ObservableCollection<OperationCategory> OperationCategories { get; set; }
        ObservableCollection<Operation> Operations { get; set; }
        ObservableCollection<Currency> Currencies { get; set; }
        ObservableCollection<Currency> CurrenciesCash { get; set; }

        ObservableCollection<AppString> AppStrings { get; set; }
        ObservableCollection<Translation> Translations { get; set; }
        ObservableCollection<Language> Languages { get; set; }

        DateTime? LastActiveDate { get; set; }
        DateTime? LastCurrencyUpdateDate { get; set; }

        

       

        void AddAccount(Account account);
        void AddOperationCategory(OperationCategory category);
        void AddOperation(Operation operation);
        void AddCurrency(Currency currency);
       

        void UpdateCurrency(Currency currency, Currency newCurrency);

        
        void Save();

        void Load();

        void EraseData();

        

        void Initialize();
    }
}
