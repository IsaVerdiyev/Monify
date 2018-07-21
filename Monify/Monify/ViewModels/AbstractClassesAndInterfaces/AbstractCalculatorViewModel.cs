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


namespace Monify.ViewModels.AbstractClassesAndInterfaces
{

    abstract class AbstractCalculatorViewModel: ObservableObject,  IViewModel, ICalculatorUser
    {

        string textBoxNumber;

        public string TextBoxNumber { get => textBoxNumber; set => SetProperty(ref textBoxNumber, value); }


        AbstractCalculatorState calculatorState;

        ICalculatorHistory calculatorHistory;

      

        public AbstractCalculatorState CalculatorState { get => calculatorState; set => calculatorState = value; }
     
        public ICalculatorHistory CalculatorHistory { get => calculatorHistory; set => calculatorHistory = value; }


        public abstract string PerformOperationButtonName { get; }


        public abstract RelayCommand PerformOperationButtonCommand { get; }


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
                            operation = new SumOperation { SecondArgument = second };
                        }
                        else if (opText == "-")
                        {
                            operation = new SubstractOperation { SecondArgument = second };
                        }
                        else if (opText == "*")
                        {
                            operation = new MultiplyOperation { SecondArgument = second };
                        }
                        else if (opText == "÷")
                        {
                            operation = new DevideOperation { SecondArgument = second };
                        }
                        else if (opText == "=")
                        {
                            operation = new EqualsOperation { SecondArgument = second };
                        }
                        else
                        {
                            operation = null;
                        }
                        TextBoxNumber = calculatorState.performOperation(operation)?.ToString() ?? TextBoxNumber;
                    }));
            }
        }

        private RelayCommand eraseCommand;

        public RelayCommand EraseCommand
        {
            get
            {
                return eraseCommand ??
                    (eraseCommand = new RelayCommand(obj =>
                    {
                        TextBoxNumber = TextBoxNumber.Remove(TextBoxNumber.Length - 1);
                    },
                    obj => TextBoxNumber != "" && !(CalculatorState is FirstArgumentEnteringCalculatorState && CalculatorState.Reset) && !(CalculatorState is InitialCalculatorState)
                    ));
            }
        }




        public abstract IViewModel ResetToInitialState();
    }
}
