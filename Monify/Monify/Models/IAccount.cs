using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    interface IAccount
    {
        string Name { get; set; }
        double Balance { get; set; }
        int? CurrencyIndex { get; set; }
        string Icon { get; set; }
    }
}
