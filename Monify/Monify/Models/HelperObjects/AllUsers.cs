using Monify.Models;
using Monify.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models.HelperObjects
{
    class AllUsers : AbstractAccount
    {
        public DoubleWrapper balance;

        public AllUsers()
        {
            index = -1;
        }

        public override double? Balance { get => balance.Value; set { } }

        

        
    }
}
