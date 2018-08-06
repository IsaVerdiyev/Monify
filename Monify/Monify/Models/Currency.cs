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
        int id;
        string code;
        double value;

        public Currency()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }
        public string Code { get => code; set => code = value; }
        public double Value { get => value; set => this.value = value; }

        public override string ToString()
        {
            return Code;
        }
    }
}
