using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.TranslatorService
{
    class RealTranslator : ITranslator
    {
        YandexTranslator translator;
        IStorage storage;

        public RealTranslator()
        {
            translator = new YandexTranslator("trnsl.1.1.20180807T091901Z.1b0f43596bce8bc2.10079c6b17425b2a5a772fd5b21a290c899fa2cc");
            storage = StorageGetter.Storage;
        }

        public string Translate(string word)
        {
            return translator.Translate(word, "en", storage.ChosenLanguage.Code);
        }
    }
}
