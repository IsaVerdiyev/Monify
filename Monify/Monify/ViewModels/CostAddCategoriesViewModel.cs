using Monify.Models;
using Monify.Services;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class CostAddCategoriesViewModel : IViewModel
    {
        IStorage storage;

        public CostAddCategoriesViewModel()
        {
           storage = StorageGetter.Storage;
        }

        public ObservableCollection<OperationCategory> ExpenseCategories { get => new ObservableCollection<OperationCategory>(storage.OperationCategories.Where(cat => cat.OperationTypeIndex == (storage.OperationTypes.FirstOrDefault(T => T.Name == "Expense").Index))); }

        public IViewModel ResetToInitialState()
        {
            return this;
        }
    }
}
