using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.Models;

namespace Monify.Services
{
    class FileDataStorage : IStorage
    {
        static FileDataStorage storage;

        ObservableCollection<Expense> expenses;

        private FileDataStorage()
        {
            expenses = new ObservableCollection<Expense>();
        }

        static FileDataStorage Storage { get => storage ?? (storage = new FileDataStorage()); }
        public ObservableCollection<Expense> Expenses {
            get => expenses;
            set => expenses = value;
        }
    }
}
