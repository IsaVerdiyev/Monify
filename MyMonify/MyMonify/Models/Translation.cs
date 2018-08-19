using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Models
{
    class Translation
    {
        static int iterator = 0;

        int id;
        string result;
        int id_Lang;
        int id_Word;

        public Translation()
        {
            id = iterator++;
        }

        public int Id { get => id; private set => id = value; }
        public string Result { get => result; set => result = value; }
        public int Id_Lang { get => id_Lang; set => id_Lang = value; }
        public int Id_Word { get => id_Word; set => id_Word = value; }
    }
}
