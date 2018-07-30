using Monify.Models;
using Monify.Services;
using Monify.Tools;
using Monify.ViewModels.AbstractClassesAndInterfaces;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monify.ViewModels
{
    class AddTransactionViewModel: ObservableObject, IViewModel
    {
        public IStorage Storage { get; }
        public string HeaderText { get => "Add Transaction"; }

        UserControl currentControl;
        DateTime selectedDate;
        private Account sourceAccount;
        private Account destinationAccount;


        public AddTransactionViewModel()
        {
            Storage = StorageGetter.Storage;
            ResetToInitialState();
        }



        public UserControl CurrentControl {
            get => currentControl;
            set {
                SetProperty(ref currentControl, value);
                CurrentControl.DataContext = this;
            }
        }

        public DateTime SelectedDate { get => selectedDate; set => SetProperty(ref selectedDate, value); }

        public Account SourceAccount { get => sourceAccount; set => SetProperty(ref sourceAccount, value); }
        public Account DestinationAccount { get => destinationAccount; set => SetProperty(ref destinationAccount, value); }

        RelayCommand returnToMainViewCommand;

        public RelayCommand ReturnToMainViewCommand
        {
            get
            {
                return returnToMainViewCommand ??
                    (returnToMainViewCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.GetViewModel(typeof(WindowViewModel).Name))).CurrentControl = new MainView();
                    }));
            }
        }

        public IViewModel ResetToInitialState()
        {
            CurrentControl = new AddTransactionDownerPartAccountChooseSubView();
            SelectedDate = DateTime.Now;

            return this;
        }
    }
}
