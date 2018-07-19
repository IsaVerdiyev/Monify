using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class CalculatorViewModel : ObservableObject, IViewModel
    {
        public IViewModel ResetToInitialState()
        {
            return this;
        }

        string operationButtonName;

        public String OperationButtonName { get => operationButtonName; set => SetProperty(ref operationButtonName, value); }
    }
}
