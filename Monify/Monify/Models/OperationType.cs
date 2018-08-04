using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    public enum OperationTypesEnum { Profit, Expense }

    class OperationType
    {
        static int iterator = 0;
        int id;
        string name;

        public OperationType()
        {
            id = iterator++;
        }

        public int Id { get => id; set => id = value; }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
