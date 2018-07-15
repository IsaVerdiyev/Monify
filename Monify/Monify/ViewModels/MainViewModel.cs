using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.ViewModels
{
    class MainViewModel: ObservableObject
    {
        public IStorage storage;


        public MainViewModel()
        {
            storage = FileDataStorage.Storage;
           
        }

        public DateTime CurrentDate { get => DateTime.Now; }

        public DateTime Yesterday { get => CurrentDate.AddDays(-1); }

        public DayOfWeek dayOfWeek { get => CurrentDate.DayOfWeek; }
    }
}
