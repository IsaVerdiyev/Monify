using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services.CalculatorService
{
    interface ICalculatorHistory
    {
        List<ICalculationOperation> History { get; set; }

        void AddToHistory(ICalculationOperation calculationOperation);
        void ClearHistory();
    }
}
