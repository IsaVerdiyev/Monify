using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.Models;

namespace Monify.ViewModels
{
    interface IGetSpecifiedGategories: IViewModel
    {
        ObservableCollection<OperationCategory> GetSpecifiedCategories { get; }
    }
}
