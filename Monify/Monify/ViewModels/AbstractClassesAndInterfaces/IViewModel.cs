using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels.AbstractClassesAndInterfaces
{
    interface IViewModel
    {
        IViewModel ResetToInitialState();
    }
}
