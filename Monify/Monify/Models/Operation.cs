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

        int id;
        int operationCategoryIndex;
        double amount;
        DateTime date;
        int accountIndex;


        public Operation()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }

        
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
