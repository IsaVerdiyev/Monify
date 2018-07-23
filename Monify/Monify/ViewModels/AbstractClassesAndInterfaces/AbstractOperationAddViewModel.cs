using Monify.Models;
using Monify.Services;
using Monify.Services.CalculatorService;
using Monify.Tools;
using Monify.ViewModels;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monify.ViewModels.AbstractClassesAndInterfaces
{
    abstract class AbstractOperationAddViewModel : AbstractCalculatorViewModel
    {
        public IStorage Storage { get; }

        public AbstractOperationAddViewModel()
        {
            Storage = StorageGetter.Storage;
            PerformOperationButtonName = "Choose category";

            ResetToInitialState();
        }

        OperationCategory selectedCategory;

        public OperationCategory SelectedCategory { get => selectedCategory; set => SetProperty(ref selectedCategory, value); }

        UserControl currentControl;

        public UserControl CurrentControl { get => currentControl; set => SetProperty(ref currentControl, value); }

        DateTime selectedDate;

        public DateTime SelectedDate { get => selectedDate; set => SetProperty(ref selectedDate, value); }

        AccountFullInfoClass selectedAccount;

        public AccountFullInfoClass SelectedAccount { get => selectedAccount; set => SetProperty(ref selectedAccount, value); }

        public ObservableCollection<AccountFullInfoClass> Accounts {
            get => new ObservableCollection<AccountFullInfoClass>(
                Storage.Accounts.
                Select(a => new AccountFullInfoClass {
                    Account = a, Code = Storage.Currencies.FirstOrDefault(c => c.Index == a.CurrencyIndex).Code
                })
            );
        }


        protected abstract Func<double> BalanceRefresher { get;}


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

        RelayCommand performOperationButtonCommand;

        public override RelayCommand PerformOperationButtonCommand
        {
            get
            {
                return performOperationButtonCommand ??
                    (performOperationButtonCommand = new RelayCommand(obj =>
                    {
                        CurrentControl = new CategoryChooseSubView();
                    },
                    obj => (CalculatorState is InitialCalculatorState || (CalculatorState is FirstArgumentEnteringCalculatorState && CalculatorState.Reset == false)) && TextBoxNumber != ""
                    ));
            }
        }

        private RelayCommand addOperationCommand;

        public RelayCommand AddOperationCommand
        {
            get
            {
                return addOperationCommand ??
                    (addOperationCommand = new RelayCommand(obj =>
                    {
                        Operation operation = new Operation
                        {
                            Amount = Double.Parse(TextBoxNumber),
                            OperationCategoryIndex = selectedCategory.Index,
                            Date = SelectedDate,
                            AccountIndex = SelectedAccount.Account.Index
                        };
                        Storage.Operations.Add(operation);
                        BalanceRefresher();
                        ReturnToMainViewCommand.Execute(obj);
                    },
                    obj => SelectedCategory != null && SelectedAccount != null
                    ));
            }
        }


        public override string PerformOperationButtonName { get; }

        public abstract string HeaderText { get; }


        public override IViewModel ResetToInitialState()
        {

            CurrentControl = new CalculatorSubView();
            CalculatorState = new InitialCalculatorState(this);
            SelectedCategory = null;
            TextBoxNumber = "";
            SelectedDate = DateTime.Now;
            SelectedAccount = null;
            return this;
        }


    }
}
