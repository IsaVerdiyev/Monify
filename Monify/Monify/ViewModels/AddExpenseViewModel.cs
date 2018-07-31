using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Monify.Models;
using Monify.Tools;
using Monify.ViewModels.AbstractClassesAndInterfaces;

namespace Monify.ViewModels
{
    class AddExpenseViewModel : AbstractOperationAddViewModel, IGetSpecifiedGategories
    {

        public ObservableCollection<OperationCategory> GetSpecifiedCategories
        {
            get
            {
                ObservableCollection<OperationCategory> expenseCategories;
                expenseCategories = new ObservableCollection<OperationCategory>(Storage.OperationCategories.Where(category => category.OperationTypeIndex == Storage.OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Index));
                expenseCategories.Remove(expenseCategories.FirstOrDefault(cat => cat.Name == OperationCategoryEnum.Transaction.ToString()));
                return expenseCategories;
            }
        }

        


        public override string HeaderText => "Add Expense";

        protected override Func<double> BalanceRefresher { get => () =>(double) (SelectedAccount.Balance -= double.Parse(TextBoxNumber));}
    }
}
