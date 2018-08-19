using MyMonify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services.TranslatorService
{
    class RealTranslator : ITranslator
    {
        YandexTranslator translator;
        IStorage storage;

        public RealTranslator(IStorage storage)
        {
            translator = new YandexTranslator("trnsl.1.1.20180807T091901Z.1b0f43596bce8bc2.10079c6b17425b2a5a772fd5b21a290c899fa2cc");
            this.storage = storage;
        }

        public string Translate(string word)
        {
            return translator.Translate(word, "en", storage.SelectedLanguage.Code);
        }

        public IList<Tuple<string, string>> GetAvailableLanguages()
        {
            return translator.GetAvailableLanguages();
        }
    }
}
