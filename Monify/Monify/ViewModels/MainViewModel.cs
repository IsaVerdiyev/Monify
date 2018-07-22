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

      

        public MainViewModel()
        {
            Storage = StorageGetter.Storage;
            ResetToInitialState();
        }

        public DateTime CurrentDate { get => DateTime.Now; }

        public DateTime Yesterday { get => CurrentDate.AddDays(-1); }

        public DayOfWeek DayOfWeek { get => CurrentDate.DayOfWeek; }

        private AbstractAccount selectedAccount;

        public AbstractAccount SelectedAccount {
            get => selectedAccount;
            set
            {
                SetProperty(ref selectedAccount, value);
                Balance = selectedAccount?.Balance ?? null;
                OperationStatistics = selectedAccount?.GetOperationsByThisAccout ?? null;
                SelectedCurrencyCode = Storage.Currencies.FirstOrDefault(c => c.Index == SelectedAccount?.CurrencyIndex)?.Code ?? " ";
            }
        }

        private ObservableCollection<string> operationStatistics;

        public ObservableCollection<string> OperationStatistics { get => operationStatistics; set => SetProperty(ref operationStatistics, value); }

        private double? balance;

        public double? Balance { get => balance; set => SetProperty(ref balance, value); }

        private string selectedCurrencyCode;
        
        public string SelectedCurrencyCode { get => selectedCurrencyCode; set => SetProperty(ref selectedCurrencyCode, value); }

        Visibility accountsControlVisibility;

        public Visibility AccountsControlVisibility { get => accountsControlVisibility; set => SetProperty(ref accountsControlVisibility, value); }

        Visibility hideAllSideMenusButtonVisibility;

        public Visibility HideAllSideMenusButtonVisibility { get => hideAllSideMenusButtonVisibility; set => SetProperty(ref hideAllSideMenusButtonVisibility, value); }

        ObservableCollection<AbstractAccount> accounts;

        public ObservableCollection<AbstractAccount> Accounts
        {
            get
            {
                accounts = new ObservableCollection<AbstractAccount>(Storage.Accounts);
                accounts.Insert(0, new AllUsers());
                return accounts;
                
            }
        }

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
                            AccountsControlVisibility = Visibility.Visible;
                        }
                        else if (AccountsControlVisibility == Visibility.Visible)
                        {
                            AccountsControlVisibility = Visibility.Collapsed;
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




        public IViewModel ResetToInitialState()
        {
            AccountsControlVisibility = Visibility.Collapsed;
            HideAllSideMenusButtonVisibility = Visibility.Collapsed;
            SelectedAccount = SelectedAccount;
            return this;
        }
    }
}
