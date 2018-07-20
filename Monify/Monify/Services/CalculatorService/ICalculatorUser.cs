using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CalculatorService
{
    interface ICalculatorUser
    {
        Double? Result { get; set; }

        AbstractCalculatorState CalculatorState { get; set; }

       
        ICalculatorHistory CalculatorHistory { get; set; }
    }
}
