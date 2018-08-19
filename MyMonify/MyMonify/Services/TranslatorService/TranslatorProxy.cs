using MyMonify.Models;
using MyMonify.Tools;
using MyMonify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyMonify.Services.TranslatorService
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

            string result;

            try
            {

                result = realTranslator.Translate(key);
            }
            catch (WebException ex)
            {
                MessageBox.Show("Error while translating due to no network connection and translations in cash. App's language is chosen English");
                storage.SelectedLanguage = storage.Languages.FirstOrDefault(l => l.Code == "en");
                return Translate(key);
            }

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
