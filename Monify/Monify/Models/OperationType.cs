using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class OperationType
    {
        static int iterator = 0;
        int index;
        string name;

        public OperationType()
        {
            index = iterator++;
        }

        public int Index { get => index; }

        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}
