using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class CostAddCategoriesViewModel
    {
        public IStorage Storage { get; }

        public CostAddCategoriesViewModel()
        {
            Storage = StorageGetter.Storage;

        }
    }
}
