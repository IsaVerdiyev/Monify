using Monify.Models;
using Monify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    class AllUsers : AbstractAccount
    {
        IStorage storage;


        public AllUsers() : base()
        {
            storage = StorageGetter.Storage;
        }



        public override double Balance { get => storage.Accounts.Sum(a => CurrencyConverter.Convert(a.CurrencyIndex.Value, this.CurrencyIndex.Value, a.Balance)); set { } }

        public override ObservableCollection<string> GetOperationsByThisAccout =>

            new ObservableCollection<string>(
            from Operation in storage.Operations
            join Category in storage.OperationCategories
            on Operation.OperationCategoryIndex equals Category.Index
            select Category.Name
            );

        public override string ToString()
        {
            return Name;
        }
    }
}
