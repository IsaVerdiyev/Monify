using MyMonify.Tools;
using MyMonify.ViewModels.AbstractClassesAndInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MyMonify.ViewModels
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
