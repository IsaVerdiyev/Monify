using Monify.Services;
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

        decimal amount;

        public Decimal Amount { get => amount; set => SetProperty(ref amount, value); }

        string textBoxNumber;

        public string TextBoxNumber { get => textBoxNumber; set => SetProperty(ref textBoxNumber, value); }

        CalculatorOperations? chosenOperation;

        private RelayCommand calculatorNumberButtonClickCommand;

        public RelayCommand CalculatorNumberButtonClickCommand
        {
            get {
                return calculatorNumberButtonClickCommand ??
                    (calculatorNumberButtonClickCommand = new RelayCommand(obj =>
                    {
                        string objText = obj as string;
                        if(Double.TryParse(objText, out double res))
                        {
                            TextBoxNumber += objText;
                        }
                    }));
            }
        }

        private RelayCommand calculatorArithmeticOperationButtonCommand;

        public RelayCommand CalculatorArithmeticOperationButtonCommand
        {
            get {
                return calculatorArithmeticOperationButtonCommand ??
                    (calculatorArithmeticOperationButtonCommand = new RelayCommand(obj =>
                    {
                       
                    }));
            }
        }


        public IViewModel ResetToInitialState()
        {
            return this;
        }
    }
}
