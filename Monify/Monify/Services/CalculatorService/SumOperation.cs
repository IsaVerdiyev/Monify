using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CalculatorService
{
    class SumOperation : ICalculationOperation
    {
        public double? FirstArgument { get ; set ; }
        public double? SecondArgument { get; set; }
        public double? Result { get ; set ; }

        public Action Operation { get => (() => Result = FirstArgument + SecondArgument); }

        public Func<bool> ValidateChecker { get => null; }
    }
}
