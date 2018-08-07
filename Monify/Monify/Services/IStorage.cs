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
        DateTime? LastActiveDate { get; }
        ObservableCollection<Currency> CurrencyCollectionFromDbSet { get; }
        DateTime? LastCurrencyUpdateDate { get; set; }

        ICurrencyGetter CurrencyGetter { get; }

       

        void AddAccount(Account account);
        void AddOperationCategory(OperationCategory category);
        void AddOperation(Operation operation);
        void AddCurrency(Currency currency);
        void AddCurrencies(ObservableCollection<Currency> currencies);

        
        void Save();

        void Load();

        void EraseData();

        void EraseCurrencies();

        void Initialize();
    }
}
