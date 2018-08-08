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

        public TranslatorProxy()
        {
            realTranslator = new RealTranslator();
            storage = StorageGetter.Storage;
        }

        public string Translate(string key)
        {
            Translation translation = storage.TranslationCash.FirstOrDefault
                (t => t.Id_Word == storage.AppStrings.FirstOrDefault(s => s.Word == key).Id
                && t.Id_Lang == storage.ChosenLanguage.Id);
            if(translation != null)
            {
                return translation.Result;
            }
            else
            {
               
                    string result = realTranslator.Translate(key);

                    translation = new Translation
                    {
                        Id_Lang = storage.ChosenLanguage.Id,
                        Id_Word = storage.AppStrings.FirstOrDefault(s => s.Word == key).Id,
                        Result = result
                    };
                    storage.AddTranslation(translation);
                    return translation.Result;
                
                
            }
        }
    }
}
