using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class Operation
    {
        static int iterator = 0;

        int index;
        int expenseCategoryIndex;
        int operationCategoryIndex;
        double amount;

        public Operation()
        {
            index = iterator++;
        }

        public int Index { get => index; }

        public int ExpenseCategoryIndex {
            get => expenseCategoryIndex;
            set => expenseCategoryIndex = value;
        }
        public int OperationCategoryIndex {
            get => operationCategoryIndex;
            set => operationCategoryIndex = value;
        }
        public double Amount {
            get => amount;
            set => amount = value;
        }


    }
}
