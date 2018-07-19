using Monify.Tools;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class CostProfitCalculatorViewModel : AbstractCalculatorViewModel
    {
       

        private RelayCommand chooseCategoryButtonCommand;

        public RelayCommand ChooseCategoryButtonCommand
        {
            get {
                return chooseCategoryButtonCommand ??
                    (chooseCategoryButtonCommand = new RelayCommand(obj =>
                    {
                        ((CostAddViewModel)ViewModelsStorage.ViewModels[VM.CostAddViewModel]).CurrentControl = new CostAddCategoriesView();
                    }));
            }
        }

    }
}
