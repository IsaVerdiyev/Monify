using Monify.Tools;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class TransactionAccountChooseSubViewModel : ObservableObject, IViewModel
    {
        public IViewModel ResetToInitialState()
        {
            return this;
        }

        private RelayCommand switchToTransactionCalculatorViewCommand;

        public RelayCommand SwitchToTransactionCalculatorViewCommand
        {
            get {
                return switchToTransactionCalculatorViewCommand ??
                    (switchToTransactionCalculatorViewCommand = new RelayCommand(obj =>
                    {
                        ((TransactionViewModel)ViewModelsStorage.ViewModels[VM.TransactionViewModel]).CurrentControl = new TransactionCalculatorView();
                    }));
            }
        }

    }
}
