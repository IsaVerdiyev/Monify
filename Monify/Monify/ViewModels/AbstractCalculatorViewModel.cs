using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    abstract class AbstractCalculatorViewModel: ObservableObject,  IViewModel
    {
        public IViewModel ResetToInitialState()
        {
            return this;
        }


        decimal amount;

        public Decimal Amount { get => amount; set => SetProperty(ref amount, value); }
    }
}
