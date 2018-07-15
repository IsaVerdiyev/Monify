using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class CostAddViewModel
    {
        public IStorage Storage { get; }

        public CostAddViewModel()
        {
            Storage = StorageGetter.Storage;
            
        }

        public DateTime CurrentDate { get => DateTime.Now; }
    }
}
