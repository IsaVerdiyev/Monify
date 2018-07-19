using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services
{
    public enum CalculatorOperations { Plus, Minus, Multiply, Devide }


    static class Calculator
    {
        public static double Calculate(double num1, double num2, CalculatorOperations operation)
        {
            if(operation == CalculatorOperations.Plus)
            {
                return num1 + num2;
            }
            else if (operation == CalculatorOperations.Minus)
            {
                return num1 - num2;
            }
            else if (operation == CalculatorOperations.Multiply)
            {
                return num1 * num2;
            }
            else if (operation == CalculatorOperations.Devide)
            {
                return num1 / num2;
            }
            else
            {
                throw new Exception("Invalid enum parameter was sent");
            }
        }
    }
}
