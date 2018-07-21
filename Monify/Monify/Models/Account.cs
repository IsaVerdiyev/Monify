using Monify.Services;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class Account : AbstractAccount
    {
        public override double Balance { get => balance; set => SetProperty(ref balance, value); }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
