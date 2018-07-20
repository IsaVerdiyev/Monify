using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CalculatorService
{
    class SecondArgumentEnteringCalculatorState : AbstractCalculatorState
    {

        public SecondArgumentEnteringCalculatorState(ICalculatorUser calculatorUser): base(calculatorUser) { }
       

        public override double? performOperation(ICalculationOperation operation)
        {

            CalculationOperation.SecondArgument = operation.SecondArgument;
            CalculationOperation.Operation();
            double result = CalculationOperation.Result.Value;
            calculatorUser.CalculatorHistory?.AddToHistory(CalculationOperation);
            if(!(operation is EqualsOperation))
            {
                operation.FirstArgument = CalculationOperation.Result;
                CalculationOperation = operation;
                Reset = true;
            }
            else
            {
                calculatorUser.CalculatorState = new InitialCalculatorState(calculatorUser);
            }
            return result;
        }

        public override void ResetVisibleInput<T>(ref T field, T value)
        {
            if (Reset)
            {
                field = value;
            }
        }
    }
}
