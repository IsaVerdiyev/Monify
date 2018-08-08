using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.TranslatorService
{
    interface ITranslator
    {
        string Translate(string word);
    }
}
