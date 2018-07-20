using Monify.Services;
using Monify.Services.CalculatorService;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace Monify.ViewModels
{

    abstract class AbstractCalculatorViewModel: ObservableObject,  IViewModel, ICalculatorUser
    {

        double? result;

        public Double? Result { get => result; set => SetProperty(ref result, value); }

        string textBoxNumber;

        public string TextBoxNumber { get => textBoxNumber; set => SetProperty(ref textBoxNumber, value); }


        AbstractCalculatorState calculatorState;

        ICalculatorHistory calculatorHistory;

      

        public AbstractCalculatorState CalculatorState { get => calculatorState; set => calculatorState = value; }
     
        public ICalculatorHistory CalculatorHistory { get => calculatorHistory; set => calculatorHistory = value; }



        private RelayCommand calculatorNumberButtonClickCommand;

        public RelayCommand CalculatorNumberButtonClickCommand
        {
            get {
                return calculatorNumberButtonClickCommand ??
                    (calculatorNumberButtonClickCommand = new RelayCommand((obj =>
                    {
                        calculatorState.ResetVisibleInput(ref textBoxNumber, "");
                        TextBoxNumber += (string)obj;
                    }),
                    obj => 
                    {
                        string text = obj as string;
                        if (text == "." && (TextBoxNumber.Contains(".") || TextBoxNumber == ""))
                        {
                            return false;
                        }
                        return true;
                    }));
            }
        }

        private RelayCommand calculatorArithmeticOperationButtonCommand;

        public RelayCommand CalculatorArithmeticOperationButtonCommand
        {
            get
            {
                return calculatorArithmeticOperationButtonCommand ??
                    (calculatorArithmeticOperationButtonCommand = new RelayCommand(obj =>
                    {
                        string opText = obj as string;
                        ICalculationOperation operation;
                        Double.TryParse(TextBoxNumber, out double second);
                        if (opText == "+")
                        {
                            operation = new SumOperation { FirstArgument = result, SecondArgument = second };
                        }
                        else if (opText == "-")
                        {
                            operation = new SubstractOperation { FirstArgument = result, SecondArgument = second };
                        }
                        else if (opText == "*")
                        {
                            operation = new MultiplyOperation { FirstArgument = result, SecondArgument = second };
                        }
                        else if (opText == "/")
                        {
                            operation = new DevideOperation { FirstArgument = result, SecondArgument = second };
                        }
                        else if (opText == "=")
                        {
                            operation = new EqualsOperation { FirstArgument = result, SecondArgument = second };
                        }
                        else
                        {
                            operation = null;
                        }
                        TextBoxNumber = calculatorState.performOperation(operation)?.ToString() ?? TextBoxNumber;
                    }));
            }
        }

        public abstract IViewModel ResetToInitialState();
    }
}
