using Monify.Models;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monify.Services.TranslatorService
{
    class TranslatorProxy: ITranslator
    {
        ITranslator realTranslator;

        IStorage storage;

        public TranslatorProxy(IStorage storage)
        {
            realTranslator = new RealTranslator(storage);
            this.storage = storage;
        }

        public IList<Tuple<string, string>> GetAvailableLanguages()
        {
            return realTranslator.GetAvailableLanguages();
        }

        public string Translate(string key)
        {
            Translation translation;
            if (storage.AppStrings.FirstOrDefault(s => s.Word == key) != null)
            {
                translation = storage.TranslationCash.FirstOrDefault
                   (t => t.Id_Word == storage.AppStrings.FirstOrDefault(s => s.Word == key).Id
                   && t.Id_Lang == storage.SelectedLanguage.Id);
                if(translation != null)
                {
                    return translation.Result;
                }
            }
            else
            {
                storage.AddAppString(new AppString { Word = key });
            }



            string result = realTranslator.Translate(key);

            translation = new Translation
            {
                Id_Lang = storage.SelectedLanguage.Id,
                Id_Word = storage.AppStrings.FirstOrDefault(s => s.Word == key).Id,
                Result = result
            };
            storage.AddTranslation(translation);
            return translation.Result;



        }
    }
}
