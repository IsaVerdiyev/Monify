using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.AbstractClassesAndInterfaces.AbstractClassesAndInterfaces.ViewModels
{
    interface IViewModel
    {
        IViewModel ResetToInitialState();
    }
}
