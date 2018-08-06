using Monify.Models;
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
        DateTime CurrenciesDate { get; set; }
        DateTime LastUpdateDate { get; set; }

        void InitializeCurrencies();

        void AddAccount(Account account);
        void AddOperationCategory(OperationCategory category);
        void AddOperation(Operation operation);

        void Save();

        void Load();

        void EraseData();

        void Initialize();
    }
}
