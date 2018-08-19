using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services.CalculatorService
{
    class DevideOperation: ICalculationOperation
    {
        public double? FirstArgument { get; set; }
        public double? SecondArgument { get; set; }
        public double? Result { get; set; }

        public Action Operation { get => (() => Result = FirstArgument + SecondArgument); }

        public Func<bool> ValidateChecker { get => (() => SecondArgument != 0); }
    }
}
