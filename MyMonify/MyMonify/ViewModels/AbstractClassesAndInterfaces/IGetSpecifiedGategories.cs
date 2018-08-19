using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyMonify.Models;

namespace MyMonify.ViewModels.AbstractClassesAndInterfaces
{
    interface IGetSpecifiedGategories: IViewModel
    {
        ObservableCollection<OperationCategory> GetSpecifiedCategories { get; }
    }
}
