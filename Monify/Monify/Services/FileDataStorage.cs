using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Monify.Models;
using Monify.Tools;

namespace Monify.Services
{
    class FileDataStorage :ObservableObject, IStorage
    {
        static FileDataStorage storage;

        

        ObservableCollection<OperationCategory> operationCategories;
        ObservableCollection<OperationType> operationTypes;
        ObservableCollection<Operation> operations;
        ObservableCollection<AbstractAccount> accounts;
        ObservableCollection<Currency> currencies;

        private FileDataStorage()
        {
            Initialize();
        }

        public static FileDataStorage Storage { get => storage ?? (storage = new FileDataStorage()); }
       
        public ObservableCollection<AbstractAccount> Accounts { get => accounts; set => SetProperty(ref accounts,value); }
        public ObservableCollection<OperationType> OperationTypes { get => operationTypes; set => SetProperty(ref operationTypes, value); }
        public ObservableCollection<OperationCategory> OperationCategories { get => operationCategories; set => SetProperty(ref operationCategories, value); }
        public ObservableCollection<Operation> Operations { get => operations; set => SetProperty(ref operations, value); }
        public ObservableCollection<Currency> Currencies { get => currencies; set => SetProperty(ref currencies, value); }

        public void Initialize()
        {
            OperationTypes = new ObservableCollection<OperationType>
            {
                new OperationType{Name = "Expense"} ,
                new OperationType{Name = "Profit"}
            };

            OperationCategories = new ObservableCollection<OperationCategory>
            {
                new OperationCategory{
                    Name = "Hygiene",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory
                {
                    Name = "Food",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Accommodation",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },

                new OperationCategory{
                    Name = "Health",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Cafe",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Car",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Clothes",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Pets",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Presents",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Entertainments",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Communication",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Sports",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Taxi",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Transport",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Deposits",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Profit").Index
                },
                new OperationCategory{
                    Name = "Salary",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Profit").Index
                },
                new OperationCategory{
                    Name = "Saving",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Profit").Index
                }
            };

            Accounts = new ObservableCollection<AbstractAccount>();

            Operations = new ObservableCollection<Operation>();

            Currencies = new ObservableCollection<Currency>();
            SetCurrencies();


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

        public void SetCurrencies()
        {
            string xmlData = GetXmlOfCurrencies();

            var doc = new XmlDocument();
            doc.LoadXml(xmlData);

            

            var nodes = doc.SelectNodes("/ValCurs/ValType[@Type='Xarici valyutalar']/Valute");
            foreach (XmlNode item in nodes)
            {
                Currency currency = new Currency
                {
                    Code = item.Attributes["Code"].InnerText.ToUpper(),
                    Value = Double.Parse(item["Value"].InnerText)
                };


                Currency searchedCurrency = Currencies.FirstOrDefault(c => c.Code == currency.Code);
                if (searchedCurrency != null)
                {
                    searchedCurrency.Value = currency.Value;
                }
                else
                {
                    Currencies.Add(currency);
                }
            }

            if(Currencies.FirstOrDefault(c => c.Code == "AZN") == null)
            {
                Currency aznCurrency = new Currency
                {
                    Code = "AZN",
                    Value = 1
                };

                Currencies.Add(aznCurrency);
            }
        }


    }
}
