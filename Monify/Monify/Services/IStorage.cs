﻿using Monify.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services
{
    interface IStorage
    {
        ObservableCollection<IAccount> Accounts { get; set; }
        ObservableCollection<OperationType> OperationTypes { get; set; }
        ObservableCollection<OperationCategory> OperationCategories { get; set; }
        ObservableCollection<Operation> Operations { get; set; }
        ObservableCollection<Currency> Currencies { get; set; }

        void Initialize();
    }
}
