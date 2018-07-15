using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class OperationCategory
    {
        int iterator = 0;
        int index;
        string name;
        int operationTypeIndex;

        public OperationCategory()
        {
            index = iterator++;
        }

        public int Index { get => index; }
        public string Name {
            get => name;
            set => name = value;
        }
        public int OperationTypeIndex {
            get => operationTypeIndex;
            set => operationTypeIndex = value;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
