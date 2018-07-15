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
        IStorage storage;

        public CostAddViewModel()
        {
            storage = StorageGetter.Storage;
        }

        public DateTime CurrentDate { get => DateTime.Now; }
    }
}
