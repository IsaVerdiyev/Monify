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
    class AddProfitViewModel : AbstractOperationAddViewModel, IGetSpecifiedGategories
    {
        public ObservableCollection<OperationCategory> GetSpecifiedCategories {
            get
            {
                ObservableCollection<OperationCategory> profitCategories;
                profitCategories = new ObservableCollection<OperationCategory>(Storage.OperationCategories.Where(category => category.OperationTypeIndex == Storage.OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id));
                profitCategories.Remove(profitCategories.FirstOrDefault(cat => cat.Name == OperationCategoryEnum.Transaction.ToString()));
                return profitCategories;
            }
        }


        protected override Func<double> BalanceRefresher { get => () =>(double) (SelectedAccount.Balance += double.Parse(TextBoxNumber)); }



        public override string HeaderText => "Add Profit";
    }
}
