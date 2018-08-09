using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Services.TranslatorService
{
    class YandexTranslator
    {
        string apiKey;
        string startOfUrl;

        public YandexTranslator(string api)
        {
            this.apiKey = api;
            startOfUrl = "https://translate.yandex.net/api/v1.5/tr.json/";
        }

        public string Translate(string word, string sourceLangCode, string destLangCode)
        {
            using (WebClient webClient = new WebClient())
            {
                webClient.Encoding = Encoding.UTF8;
                string jsonResult = GetTranslationFromInternet(word, sourceLangCode, destLangCode);
                JObject results = JObject.Parse(jsonResult);
                return (string)results["text"][0];
            }
        }

        string GetTranslationFromInternet(string word, string sourceLangCode, string destLangCode)
        {
            using (WebClient webClient = new WebClient())
            {
                string url = String.Format($"{startOfUrl}translate?key={apiKey}&text={word}&lang={sourceLangCode}-{destLangCode}");
                byte[] data = webClient.DownloadData(url);
                return Encoding.UTF8.GetString(data);
            }
        }

        string GetAvailableLanguagesFromInternet()
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadString(String.Format($"{startOfUrl}getLangs?key={apiKey}&ui=en"));

            }
        }

        public IList<Tuple<string, string>> GetAvailableLanguages()
        {
            string jsonResult = GetAvailableLanguagesFromInternet();
            JObject results = JObject.Parse(jsonResult);

            IList<Tuple<string, string>> tuples = new List<Tuple<string, string>>(JObject.Parse(results["langs"].ToString()).Properties().Select(p => new Tuple<string, string>(p.Value.ToString(), p.Name)));

            return tuples;
        }
    }
}
