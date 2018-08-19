using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services.TranslatorService
{
    interface ITranslator
    {
        string Translate(string word);

        IList<Tuple<string, string>> GetAvailableLanguages();
    }
}
