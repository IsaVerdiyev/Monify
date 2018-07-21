using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CalculatorService
{
    class FirstArgumentEnteringCalculatorState : AbstractCalculatorState
    {


        public FirstArgumentEnteringCalculatorState(ICalculatorUser calculatorUser): base(calculatorUser){}


        public override double? performOperation(ICalculationOperation operation)
        {
            operation.FirstArgument = operation.SecondArgument;
            operation.SecondArgument = null;
            this.CalculationOperation = operation;
            reset = true;
            return null;
        }

        public override void ResetVisibleInput<T>(ref T field, T value)
        {
            if(reset)
            {
                calculatorUser.CalculatorState = new SecondArgumentEnteringCalculatorState(calculatorUser);
                calculatorUser.CalculatorState.CalculationOperation = this.CalculationOperation;
                field = value;
            }
            
        }
    }
}
