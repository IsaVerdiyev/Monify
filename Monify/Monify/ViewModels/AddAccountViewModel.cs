using Monify.Models;
using Monify.Services;
using Monify.Tools;
using Monify.ViewModels.AbstractClassesAndInterfaces;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class AddAccountViewModel: ObservableObject, IViewModel
    {
        public IStorage Storage { get; }

        string newAccountName;

        ObservableCollection<string> icons;

        string selectedIcon;

        string balance;

        DateTime? selectedDate;

        Currency selectedCurrency;

        public AddAccountViewModel()
        {
            Storage = StorageGetter.Storage;
            ResetToInitialState();
        }


        public ObservableCollection<string> Icons { get => icons; set => icons = value; }

        public string NewAccountName { get => newAccountName; set => SetProperty(ref newAccountName, value); }

        public string SelectedIcon { get => selectedIcon; set => SetProperty(ref selectedIcon, value); }

        public string Balance { get => balance; set => SetProperty(ref balance, value); }

        public Currency SelectedCurrency { get => selectedCurrency; set => SetProperty(ref selectedCurrency, value); }

        public DateTime? SelectedDate { get => selectedDate; set => SetProperty(ref selectedDate, value); }

        private RelayCommand addAccountCommand;

        public RelayCommand AddAccountCommand
        {
            get
            {
                return addAccountCommand ??
                    (addAccountCommand = new RelayCommand(obj =>
                    {
                        Storage.Accounts.Add(new Account
                        {
                            Name = NewAccountName,
                            Icon = SelectedIcon,
                            Balance = Double.Parse(Balance),
                            CurrencyIndex = selectedCurrency.Index,
                            StartDate = SelectedDate.Value
                        });
                        ReturnToMainViewCommand.Execute(obj);
                    },
                    obj => SelectedIcon != null && Balance != "" && NewAccountName != "" && SelectedCurrency != null && SelectedDate != null
                    ));
            }
        }

        private RelayCommand returnToMainViewCommand;

        public RelayCommand ReturnToMainViewCommand
        {
            get
            {
                return returnToMainViewCommand ??
                    (returnToMainViewCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)ViewModelsStorage.GetViewModel(typeof(WindowViewModel).Name)).CurrentControl = new MainView();
                    }));
            }
        }



        public IViewModel ResetToInitialState()
        {
            SelectedDate = DateTime.Now;
            SelectedIcon = "";
            NewAccountName = "";
            SelectedIcon = null;
            SelectedCurrency = null;
            Icons = new ObservableCollection<string> { "💳", "💰" };
            return this;
        }
    }
}
