using Monify.Models;
using Monify.Services;
using Monify.Tools;
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

namespace Monify.ViewModels
{
    class MainViewModel: ObservableObject, IViewModel
    {
        public IStorage Storage { get; }

      

        public MainViewModel()
        {
            Storage = StorageGetter.Storage;
           
        }

        public DateTime CurrentDate { get => DateTime.Now; }

        public DateTime Yesterday { get => CurrentDate.AddDays(-1); }

        public DayOfWeek DayOfWeek { get => CurrentDate.DayOfWeek; }

        public IAccount SelectedAccount { get; set; }

        ObservableCollection<IAccount> accounts;

        public ObservableCollection<IAccount> Accounts
        {
            get
            {
                accounts = Storage.Accounts;
                accounts.Insert(0, new AllUsers());
                return accounts;
                
            }
        }

        RelayCommand addCost;

        public RelayCommand AddCost {
            get
            {
                return addCost ??
                    (addCost = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.ViewModels[VM.WindowViewModel])).CurrentControl = new CostAddView();
                    }
                    ));
            }
        }

        

       
    }
}
