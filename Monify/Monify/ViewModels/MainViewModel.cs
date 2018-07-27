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
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms.VisualStyles;
using System.Windows.Media.Animation;
using static Monify.Tools.DateTimeExtensions;

namespace Monify.ViewModels
{
    class MainViewModel: ObservableObject, IViewModel
    {
        public IStorage Storage { get; }


        int categoriesRow;
        int accountsRow;
        int currenciesRow;
        int settingsRow;

        private double? balance;

        Account selectedAccount;
        Account allUsers;
        ObservableCollection<Account> accounts;

        DateTime selectedDate;
        DateTime? startDate;
        DateTime? pastDate;
        DateTime? nextDate;
        DateInterval statisticsDateInterval;

        ObservableCollection<Operation> operationStatistics;

        
        Visibility accountsControlVisibility;
        Visibility currenciesControlVisibility;
        Visibility settingsControlVisibility;

        Visibility hideAllSideMenusButtonVisibility;
       






        public MainViewModel()
        {
            Storage = StorageGetter.Storage;
            SelectedDate = DateTime.Now;
            SelectedAccount = AllUsers;
            AllUsersCurrency = Storage.Currencies.FirstOrDefault(c => c.Code == "USD");
            StatisticsDateInterval = DateInterval.Day;
            ResetToInitialState();
        }









        public int CategoriesRow { get => categoriesRow; set => SetProperty(ref categoriesRow, value); }
        public int AccountsRow { get => accountsRow; set => SetProperty(ref accountsRow, value); }
        public int CurrenciesRow { get => currenciesRow; set => SetProperty(ref currenciesRow, value); }
        public int SettingsRow { get => settingsRow; set => SetProperty(ref settingsRow, value); }


        public double? Balance {
            get => balance;
            set
            {
                if (SelectedAccount == AllUsers)
                {
                    SetProperty(ref balance, Storage.Accounts.Sum(a => CurrencyConverter.Convert(a.CurrencyIndex.Value, AllUsers.CurrencyIndex.Value, a.Balance.Value)));
                }
                else
                {
                    SetProperty(ref balance, SelectedAccount?.Balance);
                }
            }
        }


        public Account SelectedAccount
        {
            get => selectedAccount;
            set
            {
                SetProperty(ref selectedAccount, value);
                Balance = Balance;
                StartDate = StartDate;
                if(SelectedDate < StartDate)
                {
                    SelectedDate = StartDate ?? DateTime.Now;
                }
                PastDate = PastDate;
                NextDate = NextDate;
                OperationStatistics = OperationStatistics;
            }
        }
        public Account AllUsers
        {
            get => allUsers ??
                (allUsers = new Account(-1) {
                    Name = "All Users"
                });
        }
        public Currency AllUsersCurrency {
            get => Storage.Currencies.FirstOrDefault(c => c.Index == AllUsers.CurrencyIndex);
            set
            {
                AllUsers.CurrencyIndex = (value as Currency).Index;
                OnPropertyChanged();
                Accounts = Accounts;
                SelectedAccount = SelectedAccount;
            }
        }
        public ObservableCollection<Account> Accounts { get => accounts; set => SetProperty(ref accounts, value); }


        public DateTime SelectedDate {
            get => selectedDate;
            set
            {
                SetProperty(ref selectedDate, value);
                PastDate = PastDate;
                NextDate = NextDate;
                OperationStatistics = OperationStatistics;
            }
        }
        public DateTime? StartDate {
            get => startDate;
            set
            {
                if(SelectedAccount == AllUsers)
                {
                    SetProperty(ref startDate, Storage.Accounts?.Min(a => a.StartDate));
                }
                else
                {
                    SetProperty(ref startDate, SelectedAccount?.StartDate);
                }
            }
        }
        public DateTime? PastDate {
            get => pastDate;
            set
            {

                DateTime? resultedPastDate = (DateTime?)SelectedDate.GetPastDate(StatisticsDateInterval);
                if(resultedPastDate > StartDate)
                {
                    SetProperty(ref pastDate, resultedPastDate);
                }
                else
                {
                    SetProperty(ref pastDate, null);
                }
            }
        }
        public DateTime? NextDate
        {
            get => nextDate;
            set
            {
                DateTime? resultedNextDate = (DateTime?)SelectedDate.GetNextDate(StatisticsDateInterval);
                if (resultedNextDate < DateTime.Now)
                {
                    SetProperty(ref nextDate, resultedNextDate);
                }
                else
                {
                    SetProperty(ref nextDate, null);
                }
            }
        }
        public DateInterval StatisticsDateInterval {
            get => statisticsDateInterval;
            set
            {
                SetProperty(ref statisticsDateInterval, value);
                PastDate = PastDate;
                NextDate = NextDate;
                OperationStatistics = OperationStatistics;
            }
        }


        public ObservableCollection<Operation> OperationStatistics {
            get => operationStatistics;
            set
            {
                if (SelectedAccount == AllUsers)
                {
                    SetProperty(ref operationStatistics, new ObservableCollection<Operation>(Storage.Operations.Where(o => o.Date.IsInCurrentDateInterval(SelectedDate, StatisticsDateInterval)).OrderBy(o => o.Date)));
                }
                else
                {
                    SetProperty(ref operationStatistics, new ObservableCollection<Operation>(Storage.Operations?.Where(op => op.AccountIndex == SelectedAccount?.Index && op.Date.IsInCurrentDateInterval(SelectedDate, StatisticsDateInterval)).OrderBy(o => o.Date)));
                }
            }
        }


        public Visibility AccountsControlVisibility { get => accountsControlVisibility; set => SetProperty(ref accountsControlVisibility, value); }
        public Visibility CurrenciesControlVisibility { get => currenciesControlVisibility; set => SetProperty(ref currenciesControlVisibility, value); }
        public Visibility SettingsControlVisibility { get => settingsControlVisibility; set => SetProperty(ref settingsControlVisibility, value); }

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

        private RelayCommand showHideSettingsCommand;

        public RelayCommand ShowHideSettingsCommand
        {
            get {
                return showHideSettingsCommand ??
                    (showHideSettingsCommand = new RelayCommand(obj =>
                    {
                        if (SettingsControlVisibility == Visibility.Collapsed)
                        {
                            CategoriesRow = SettingsRow;
                            SettingsRow = 0;
                            SettingsControlVisibility = Visibility.Visible;
                        }
                        else if (SettingsControlVisibility == Visibility.Visible)
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

        private RelayCommand changeDateIntervalCommand;

        public RelayCommand ChangeDateIntervalCommand {
            get
            {
                return changeDateIntervalCommand ??
                    (changeDateIntervalCommand = new RelayCommand(obj =>
                    {
                        DateInterval dateIntervalParam = (DateInterval)obj;

                        StatisticsDateInterval = dateIntervalParam;
                    }
                    ));
            }
        }


        private RelayCommand navigateToPastCommand;

        public RelayCommand NavigateToPastCommand
        {
            get {
                return navigateToPastCommand ??
                    (navigateToPastCommand = new RelayCommand(obj =>
                    {
                        SelectedDate = PastDate.Value;

                    },
                    obj => PastDate != null
                    )); 
            }
        }

        private RelayCommand navigateToNextCommand;

        public RelayCommand NavigateToNextCommand
        {
            get {
                return navigateToNextCommand ??
                    (navigateToNextCommand = new RelayCommand(obj =>
                    {
                        SelectedDate = NextDate.Value;
                    },
                    obj => NextDate != null
                    ));
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
            SettingsControlVisibility = Visibility.Collapsed;
        }


        public IViewModel ResetToInitialState()
        {
            HideAllSideMenusButtonVisibility = Visibility.Collapsed;
            ResetOtherSettingsRowsDisplay();
            var tempAccounts = new ObservableCollection<Account>(Storage.Accounts);
            tempAccounts.Add(AllUsers);
            Accounts = tempAccounts;
            SelectedAccount = SelectedAccount;
            return this;
        }
    }
}
