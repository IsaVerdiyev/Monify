using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.AbstractClassesAndInterfaces.AbstractClassesAndInterfaces.ViewModels;
using Monify.Models;

namespace Monify.AbstractClassesAndInterfaces.ViewModels
{
    interface IGetSpecifiedGategories: IViewModel
    {
        ObservableCollection<OperationCategory> GetSpecifiedCategories { get; }
    }
}
