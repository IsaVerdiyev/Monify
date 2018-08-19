using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services.CalculatorService
{
    interface ICalculationOperation
    {
        double? FirstArgument { get; set; }

        double? SecondArgument { get; set; }

        double? Result { get; set; }

        Action Operation { get; }

        Func<bool> ValidateChecker { get;}
    }
}
