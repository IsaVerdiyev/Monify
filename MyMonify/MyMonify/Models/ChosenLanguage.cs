using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Models
{
    class ChosenLanguage
    {
        static int iterator = 0;

        int id;

        int? id_Language;

        public ChosenLanguage()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }
        public int? Id_Language { get => id_Language; set => id_Language = value; }
    }
}
