using Monify.Models;
using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    class AllUsers : AbstractAccount
    {
        IStorage storage;


        public AllUsers():base()
        {
            storage = StorageGetter.Storage;
            Name = "All Users";
        }

        

        public override double Balance { get => storage.Accounts.Sum(a => CurrencyConverter.Convert(a.CurrencyIndex.Value, this.CurrencyIndex.Value, a.Balance)); set { } }
    }
}
