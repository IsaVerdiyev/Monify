using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Monify.Models;

namespace Monify.Services.CurrencyGetterService
{
    class RealCurrencyGetter : ICurrencyGetter
    {

        public ObservableCollection<Currency> Currencies {
            get
            {
                string xmlData = GetXmlOfCurrencies();

                var doc = new XmlDocument();
                doc.LoadXml(xmlData);

                ObservableCollection<Currency> currencies = new ObservableCollection<Currency>();

                var nodes = doc.SelectNodes("/ValCurs/ValType[@Type='Xarici valyutalar']/Valute");
                foreach (XmlNode item in nodes)
                {

                    currencies.Add(new Currency
                    {
                        Code = item.Attributes["Code"].InnerText.ToUpper(),
                        Value = Double.Parse(item["Value"].InnerText)
                    });
                }

                currencies.Add(new Currency
                {
                    Code = "AZN",
                    Value = 1
                });
                return currencies;
            }
        }

        string GetXmlOfCurrencies()
        {
            using (WebClient web = new WebClient())
            {
                string day = String.Format("{0,2}", DateTime.Now.Day.ToString()).Replace(' ', '0');
                string month = String.Format("{0,2}", DateTime.Now.Month.ToString()).Replace(' ', '0');
                string date = day + "." + month + "." + DateTime.Now.Year;
                string unformattedUrl = "http://www.cbar.az/currencies/" + date + ".xml";
                string url = string.Format(unformattedUrl);
                string data = web.DownloadString(url);
                return data;

            }
        }
    }
}
