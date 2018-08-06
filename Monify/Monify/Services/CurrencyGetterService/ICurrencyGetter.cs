﻿using Monify.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.CurrencyGetterService
{
    interface ICurrencyGetter
    {
        ObservableCollection<Currency> Currencies { get; }
    }
}
