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
        int operationCategoryIndex;
        double amount;
        DateTime date;
        int accountIndex;


        public Operation()
        {
            index = iterator++;
        }

        public int Index { get => index; }

        
        public int OperationCategoryIndex {
            get => operationCategoryIndex;
            set => operationCategoryIndex = value;
        }
        public double Amount {
            get => amount;
            set => amount = value;
        }

        public int AccountIndex
        {
            get => accountIndex;
            set => accountIndex = value;
        }

        public DateTime Date { get => date; set => date = value; }


    }
}
