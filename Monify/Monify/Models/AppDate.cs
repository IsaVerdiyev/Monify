using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    enum AppDateEnum { LastActiveDate, LastCurrencyUpdateDate}

    class AppDate
    {
        static int iterator = 0;
        DateTime? date;
        string name;
        int id;

        public AppDate()
        {
            id = iterator++;
        }

        public DateTime? Date { get => date; set => date = value; }
        public string Name { get => name; set => name = value; }
        public int Id { get => id; private set => id = value; }
    }
}
