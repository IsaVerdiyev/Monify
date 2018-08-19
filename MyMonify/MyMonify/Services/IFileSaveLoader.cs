using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services
{
    interface IFileSaveLoader: ISaveLoader
    {
        string SaveFileLocation { get; set; }
    }
}
