using Monify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    enum VM{
        WindowViewModel,
        MainViewModel,
        TransactionViewModel,
        CostAddViewModel,
        CostProfitCalculatorViewModel,
        TransactionCalculatorViewModel,
        TransactionAccountChooseSubViewModel,
        CostAddCategoriesViewModel
    }

    static class ViewModelsStorage
    {
        static Dictionary<VM, IViewModel> viewModels;


        public static Dictionary<VM, IViewModel> ViewModels {
            get {
                return viewModels ??
                    (viewModels = new Dictionary<VM, IViewModel>()
                    {
                        {VM.WindowViewModel, new WindowViewModel() },
                        {VM.MainViewModel, new MainViewModel() },
                        {VM.TransactionViewModel, new TransactionViewModel() },
                        {VM.CostAddViewModel, new CostAddViewModel() },
                        {VM.CostProfitCalculatorViewModel, new CostProfitCalculatorViewModel() },
                        {VM.TransactionCalculatorViewModel, new TransactionViewModel() },
                        {VM.TransactionAccountChooseSubViewModel, new  TransactionAccountChooseSubViewModel()},
                        {VM.CostAddCategoriesViewModel, new CostAddCategoriesViewModel() }
                    }

                    );
            } }
    }
}
