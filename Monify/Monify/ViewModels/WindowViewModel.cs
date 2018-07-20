using Monify.AbstractClassesAndInterfaces.AbstractClassesAndInterfaces.ViewModels;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monify.ViewModels
{
    class WindowViewModel : ObservableObject, IViewModel
    {
        UserControl currentControl;

        public UserControl CurrentControl { get => currentControl; set => SetProperty(ref currentControl, value); }

        public IViewModel ResetToInitialState()
        {
            return this;
        }
    }
}
