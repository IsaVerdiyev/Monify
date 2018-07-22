using Monify.Services;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class Account : AbstractAccount
    {
        IStorage storage;

        public Account()
        {
            storage = StorageGetter.Storage;
        }

        public override double Balance { get => balance; set => SetProperty(ref balance, value); }

        public override ObservableCollection<string> GetOperationsByThisAccout => new ObservableCollection<string>(
           storage.Operations.Join(storage.OperationCategories, o => o.OperationCategoryIndex, cat => cat.Index,
               (o, cat) => new { O = o, Cat = cat }).Where(OpAndCat => OpAndCat.O.AccountIndex == this.Index).Select(OpAndCat => OpAndCat.Cat.Name));

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
