using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class Currency
    {
        static int iterator = 0;
        int index;
        string code;
        double value;

        public Currency()
        {
            index = iterator++;
        }

        public int Index { get => index; }
        public string Code { get => code; set => code = value; }
        public double Value { get => value; set => this.value = value; }
    }
}
