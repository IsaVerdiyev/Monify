using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    enum CurrencyDateNames { LastUpdate, CurrencyDate}

    class CurrencyDate
    {
        static int iterator = 0;
        DateTime? date;
        string name;
        int id;

        public CurrencyDate()
        {
            id = iterator++;
        }

        public DateTime? Date { get => date; private set => date = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; set => id = value; }
    }
}
