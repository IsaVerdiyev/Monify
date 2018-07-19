using Monify.Services;
using Monify.Tools;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class TransactionViewModel: IViewModel
    {
        public IStorage Storage { get; }



        public TransactionViewModel()
        {
            Storage = StorageGetter.Storage;
        }

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
    }
}
