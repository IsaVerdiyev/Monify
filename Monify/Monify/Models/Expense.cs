using Monify.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class Expense: ObservableObject
    {
        ExpensesCategory expensesCategory;

        public ExpensesCategory ExpensesCategory {
            get => expensesCategory;
            set => SetProperty(expensesCategory, value);
        }

        double amount;
            
        double Amount {
            get => amount;
            set => SetProperty(amount, value);
        }


       
    }
}
