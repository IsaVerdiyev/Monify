using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    

    class AppString
    {
        static int iterator = 0;
        int id;
        string word;

        public AppString()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }
        public string Word { get => word; set => word = value; }
    }
}
