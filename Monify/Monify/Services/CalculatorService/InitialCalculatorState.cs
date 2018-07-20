using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CalculatorService
{
    class InitialCalculatorState : AbstractCalculatorState
    {
        public override double? performOperation(ICalculationOperation operation)
        {
            return null;
        }

        public InitialCalculatorState(ICalculatorUser calculatorUser): base(calculatorUser) { }
      

        public override void ResetVisibleInput<T>(ref T field, T value)
        {
            calculatorUser.CalculatorState = new FirstArgumentEnteringCalculatorState(calculatorUser);
            field = value;
        }
    }
}
