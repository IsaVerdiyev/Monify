using Monify.Models;
using Monify.Services;
using Monify.Tools;
using Monify.ViewModels.AbstractClassesAndInterfaces;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Animation;

namespace Monify.ViewModels
{
    class MainViewModel: ObservableObject, IViewModel
    {
        public IStorage Storage { get; }


        int categoriesRow;
        int accountsRow;
        int currenciesRow;
        int settingsRow;

        
       

        public int CategoriesRow { get => categoriesRow; set => SetProperty(ref categoriesRow, value); }
        public int AccountsRow { get => accountsRow; set => SetProperty(ref accountsRow, value); }
        public int CurrenciesRow { get => currenciesRow; set => SetProperty(ref currenciesRow, value); }
        public int SettingsRow { get => settingsRow; set => SetProperty(ref settingsRow, value); }

        public MainViewModel()
        {
            Storage = StorageGetter.Storage;

            SelectedDate = DateTime.Now;
            Duration = TimeSpan.FromDays(1);
            ResetToInitialState();
        }

        //public DateTime CurrentDate { get => DateTime.Now; }

        //public DateTime Yesterday { get => CurrentDate.AddDays(-1); }

        public DayOfWeek DayOfWeek { get => SelectedDate.DayOfWeek; }


        DateTime selectedDate;

        public DateTime SelectedDate { get => selectedDate; set => SetProperty(ref selectedDate, value); }



        private TimeSpan duration;

        public TimeSpan Duration { get => duration; set => SetProperty(ref duration, value); }


        public DateTime Past { get => SelectedDate - Duration; }
       
            


        Account selectedAccount;

        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                SetProperty(ref selectedAccount, value);
                Balance = selectedAccount?.Balance ?? null;
                OperationStatistics = new ObservableCollection<string>(
                Storage.Operations.Join(Storage.OperationCategories, o => o.OperationCategoryIndex, cat => cat.Index,
                (o, cat) => new { O = o, Cat = cat }).Where(OpAndCat => OpAndCat.O.AccountIndex == selectedAccount.Index).Select(OpAndCat => OpAndCat.Cat.Name));
            }
        }

     

        public ObservableCollection<Account> Accounts
        {
            get
            {
                var collection = new ObservableCollection<Account>(Storage.Accounts);
                collection.Add(AllUsers);
                return collection;
            }
        }


        Account allUsers;

        public Account AllUsers
        {
            get => allUsers ??
                (allUsers = new Account(-1) { Name = "All Users", CurrencyIndex = Storage.Currencies.FirstOrDefault(c => c.Code == "USD").Index});
        }


        private ObservableCollection<string> operationStatistics;

        public ObservableCollection<string> OperationStatistics { get => operationStatistics; set => SetProperty(ref operationStatistics, value); }

        private double? balance;

        public double? Balance { get => balance; set => SetProperty(ref balance, value); }

        Visibility accountsControlVisibility;

        public Visibility AccountsControlVisibility { get => accountsControlVisibility; set => SetProperty(ref accountsControlVisibility, value); }

        Visibility currenciesControlVisibility;

        public Visibility CurrenciesControlVisibility { get => currenciesControlVisibility; set => SetProperty(ref currenciesControlVisibility, value); }

        Visibility hideAllSideMenusButtonVisibility;

        public Visibility HideAllSideMenusButtonVisibility { get => hideAllSideMenusButtonVisibility; set => SetProperty(ref hideAllSideMenusButtonVisibility, value); }


        RelayCommand addExpenseCommand;

        public RelayCommand AddExpenseCommand {
            get
            {
                return addExpenseCommand ??
                    (addExpenseCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.GetViewModel(typeof(WindowViewModel).Name))).CurrentControl = new AddExpenseView();
                    }
                    , obj => Storage.Accounts.Count != 0
                    ));
            }
        }

        RelayCommand addProfitCommand;

        public RelayCommand AddProfitCommand
        {
            get
            {
                return addProfitCommand ??
                    (addProfitCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.GetViewModel(typeof(WindowViewModel).Name))).CurrentControl = new AddProfitView();
                    },
                    obj => Storage.Accounts.Count != 0
                    ));
            }
        }

        RelayCommand hideVisibilitySettingsCommand;

        public RelayCommand HideVisibilitySettingsCommand
        {
            get
            {
                return hideVisibilitySettingsCommand ??
                    (hideVisibilitySettingsCommand = new RelayCommand(obj =>
                    {
                        MainView userControl = obj as MainView;
                        if (userControl != null)
                        {
                            var storyboard = new Storyboard();
                            var animation = new ThicknessAnimation();
                            animation.BeginTime = new TimeSpan(0);
                            Storyboard.SetTargetName(animation, userControl.VisibilitySettingsStackPanel.Name);
                            Storyboard.SetTargetProperty(animation, new PropertyPath(StackPanel.MarginProperty));
                            animation.To = new Thickness(
                                -userControl.VisibilitySettingsStackPanel.ActualWidth, 0, 0, 0
                                );
                            animation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                            storyboard.Children.Add(animation);
                            storyboard.Begin(userControl);
                        }
                    }
                    ));
            }
        }

        RelayCommand showVisibilitySettingsCommand;

        public RelayCommand ShowVisibilitySettingsCommand
        {
            get
            {
                return showVisibilitySettingsCommand ??
                    (showVisibilitySettingsCommand = new RelayCommand(obj =>
                    {
                        MainView userControl = obj as MainView;
                        if (userControl != null)
                        {
                            var storyboard = new Storyboard();
                            var animation = new ThicknessAnimation();
                            animation.BeginTime = new TimeSpan(0);
                            Storyboard.SetTargetName(animation, userControl.VisibilitySettingsStackPanel.Name);
                            Storyboard.SetTargetProperty(animation, new PropertyPath(StackPanel.MarginProperty));
                            animation.To = new Thickness(
                                0, 0, -userControl.VisibilitySettingsStackPanel.ActualWidth, 0
                                );
                            animation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                            storyboard.Children.Add(animation);
                            storyboard.Begin(userControl);
                        }
                    }));
            }
        }


        RelayCommand showOtherSettingsCommand;

        public RelayCommand ShowOtherSettingsCommand
        {
            get
            {
                return showOtherSettingsCommand ??
                    (showOtherSettingsCommand = new RelayCommand(obj =>
                    {
                        MainView userControl = obj as MainView;
                        if (userControl != null)
                        {
                            var storyboard = new Storyboard();
                            var animation = new ThicknessAnimation();
                            animation.BeginTime = new TimeSpan(0);
                            Storyboard.SetTargetName(animation, userControl.OtherSettingsStackPanel.Name);
                            Storyboard.SetTargetProperty(animation, new PropertyPath(StackPanel.MarginProperty));
                            animation.To = new Thickness(
                                -userControl.OtherSettingsStackPanel.ActualWidth, 0, 0, 0
                                );
                            animation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                            storyboard.Children.Add(animation);
                            storyboard.Begin(userControl);
                        }
                    }));
            }
        }

        RelayCommand hideOtherSettingsCommand;

        public RelayCommand HideOtherSettingsCommand
        {
            get
            {
                return hideOtherSettingsCommand ??
                    (hideOtherSettingsCommand = new RelayCommand(obj =>
                    {
                        MainView userControl = obj as MainView;
                        if (userControl != null)
                        {
                            var storyboard = new Storyboard();
                            var animation = new ThicknessAnimation();
                            animation.BeginTime = new TimeSpan(0);
                            Storyboard.SetTargetName(animation, userControl.OtherSettingsStackPanel.Name);
                            Storyboard.SetTargetProperty(animation, new PropertyPath(StackPanel.MarginProperty));
                            animation.To = new Thickness(
                                0, 0, -userControl.OtherSettingsStackPanel.ActualWidth, 0
                                );
                            animation.Duration = new Duration(TimeSpan.FromSeconds(0.2));
                            storyboard.Children.Add(animation);
                            storyboard.Begin(userControl);
                            ResetOtherSettingsRowsDisplay();
                        }
                    }));
            }
        }


        RelayCommand visibilitSettingsButtonCommands;

        public RelayCommand VisibilitySettingsButtonCommands
        {
            get
            {
                return visibilitSettingsButtonCommands ??
                    (visibilitSettingsButtonCommands = new RelayCommand(obj =>
                    {
                        ShowVisibilitySettingsCommand.Execute(obj);
                        HideOtherSettingsCommand.Execute(obj);
                        HideAllSideMenusButtonVisibility = Visibility.Visible;
                    }));
            }
        }


        private RelayCommand otherSettingsButtonCommands;

        public RelayCommand OtherSettingsButtonCommands
        {
            get {
                return otherSettingsButtonCommands ??
                    (otherSettingsButtonCommands = new RelayCommand(obj =>
                    {
                        ShowOtherSettingsCommand.Execute(obj);
                        HideVisibilitySettingsCommand.Execute(obj);
                        HideAllSideMenusButtonVisibility = Visibility.Visible;
                    }));
            }
        }


        private RelayCommand hideSideMenusButtonCommand;

        public RelayCommand HideSideMenuButtonCommand
        {
            get {
                return hideSideMenusButtonCommand ??
                    (hideSideMenusButtonCommand = new RelayCommand(obj =>
                    {
                        HideVisibilitySettingsCommand.Execute(obj);
                        HideOtherSettingsCommand.Execute(obj);
                    }));
            }
        }

        private RelayCommand openTransactionMenuCommand;

        public RelayCommand OpenTransactionMenuCommand
        {
            get { return openTransactionMenuCommand ??
                    (openTransactionMenuCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.GetViewModel(typeof(WindowViewModel).Name))).CurrentControl = new TransactionView();
                    })); }
        }

        RelayCommand showHideAccountsCommand;

        public RelayCommand ShowHideAccountsCommand
        {
            get
            {
                return showHideAccountsCommand ??
                    (showHideAccountsCommand = new RelayCommand(obj =>
                    {
                      
                        if(AccountsControlVisibility == Visibility.Collapsed)
                        {
                            CategoriesRow = AccountsRow;
                            AccountsRow = 0;
                            AccountsControlVisibility = Visibility.Visible;
                        }
                        else if (AccountsControlVisibility == Visibility.Visible)
                        {
                            ResetOtherSettingsRowsDisplay();
                        }
                    }));
            }
        }

        private RelayCommand showHideCurrenciesCommand;

        public RelayCommand ShowHideCurrenciesCommand
        {
            get {
                return showHideCurrenciesCommand ??
                    (showHideCurrenciesCommand = new RelayCommand(obj =>
                    {

                        if (CurrenciesControlVisibility == Visibility.Collapsed)
                        {
                            CategoriesRow = CurrenciesRow;
                            CurrenciesRow = 0;
                            CurrenciesControlVisibility = Visibility.Visible;
                        }
                        else if (CurrenciesControlVisibility == Visibility.Visible)
                        {
                            ResetOtherSettingsRowsDisplay();
                        }
                    }));
            }
        }


        private RelayCommand addAccountCommand;

        public RelayCommand AddAccountCommand
        {
            get {
                return addAccountCommand ??
                    (addAccountCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)ViewModelsStorage.GetViewModel(typeof(WindowViewModel).Name)).CurrentControl = new AddAccountView();
                    }));
            }
            
        }

        //private RelayCommand refreshCurrenciesCommand;

        //public RelayCommand RefreshCurrenciesCommand
        //{
        //    get
        //    {
        //        return refreshCurrenciesCommand ??
        //            (refreshCurrenciesCommand = new RelayCommand(obj =>
        //            {
        //                Storage.SetCurrencies();
        //            }));
        //    }
        //}


        private RelayCommand hideAllSideMenuButtonCommand;

        public RelayCommand HideAllSideMenuButtonCommand
        {
            get
            {
                return hideAllSideMenuButtonCommand ??
                    (hideAllSideMenuButtonCommand = new RelayCommand(obj =>
                    {
                        HideOtherSettingsCommand.Execute(obj);
                        HideVisibilitySettingsCommand.Execute(obj);
                        HideAllSideMenusButtonVisibility = Visibility.Collapsed;
                    }));
            }
        }


        void ResetOtherSettingsRowsDisplay()
        {
            CategoriesRow = 0;
            AccountsRow = 1;
            CurrenciesRow = 2;
            SettingsRow = 3;

            AccountsControlVisibility = Visibility.Collapsed;
            CurrenciesControlVisibility = Visibility.Collapsed;
        }


        public IViewModel ResetToInitialState()
        {
            AllUsers.Balance = Storage.Accounts.Sum(a => CurrencyConverter.Convert(a.CurrencyIndex.Value, AllUsers.CurrencyIndex.Value, a.Balance.Value));
            
            HideAllSideMenusButtonVisibility = Visibility.Collapsed;
            ResetOtherSettingsRowsDisplay();
            SelectedAccount = SelectedAccount;
            return this;
        }
    }
}
