using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.TranslatorService
{
    class TranslatorCash: ITranslator
    {
        ITranslator realTranslator;

        IStorage storage;

        public TranslatorCash()
        {
            realTranslator = new RealTranslator();
            storage = StorageGetter.Storage;
        }

        public string GetWord(string key)
        {
            throw new NotImplementedException();
        }
    }
}
