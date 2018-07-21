using Monify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CalculatorService
{
    abstract class AbstractCalculatorState
    {

        protected ICalculatorUser calculatorUser;

        public AbstractCalculatorState(ICalculatorUser calculatorUser)
        {
            this.calculatorUser = calculatorUser;
            reset = false;
        }

        public abstract double? performOperation(ICalculationOperation operation);

        public abstract void ResetVisibleInput<T>(ref T field, T value);

        ICalculationOperation calculationOperation;

        public ICalculationOperation CalculationOperation { get=> calculationOperation; set => calculationOperation = value; }

        protected bool reset;

        public bool Reset { get => reset; }


    }
}
