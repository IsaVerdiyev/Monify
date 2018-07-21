using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Monify.Models;
using Monify.ViewModels.AbstractClassesAndInterfaces;

namespace Monify.ViewModels
{
    class AddExpenseViewModel : AbstractOperationAddViewModel, IGetSpecifiedGategories
    {
        public ObservableCollection<OperationCategory> GetSpecifiedCategories => new ObservableCollection<OperationCategory>(Storage.OperationCategories.Where(category => category.OperationTypeIndex == Storage.OperationTypes.FirstOrDefault(t => t.Name == "Expense").Index));

        public override string HeaderText => "Add Extense";
    }
}
