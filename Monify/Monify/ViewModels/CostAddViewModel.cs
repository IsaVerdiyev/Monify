using Monify.Services;
using Monify.Tools;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monify.ViewModels
{
    class CostAddViewModel:ObservableObject, IViewModel
    {
        public IStorage Storage { get; }

        public CostAddViewModel()
        {
            Storage = StorageGetter.Storage;
            
        }

        UserControl currentControl;

        public UserControl CurrentControl { get => currentControl; set => SetProperty(ref currentControl, value); }

        public DateTime CurrentDate { get => DateTime.Now; }

        RelayCommand returnToMainViewCommand;

        public RelayCommand ReturnToMainViewCommand
        {
            get
            {
                return returnToMainViewCommand ??
                    (returnToMainViewCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.ViewModels[VM.WindowViewModel])).CurrentControl = new MainView();
                    }));
            }
        }

        public IViewModel ResetToInitialState()
        {
            CurrentControl = new CalculatorView();
            ((CalculatorViewModel)ViewModelsStorage.ViewModels[VM.CalculatorViewModel]).OperationButtonName = "Choose category";

            return this;
        }
    }
}
