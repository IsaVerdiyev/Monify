using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class ExpensesCategory: ObservableObject
    {
        string name;

        public string Name {
            get => name;
            set => SetProperty(name, value);
        }

        string imagePath;

        public string ImagePath {
            get => imagePath;
            set => SetProperty(imagePath, value);
        }
    }
}
