using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.Models;

namespace Monify.Services.CurrencyGetterService
{
    class ProxyCurrencyGetter : ICurrencyGetter
    {
        IStorage storage;

        ICurrencyGetter realCurrencyGetter;

        public ProxyCurrencyGetter(IStorage storage)
        {
            this.storage = storage;
            realCurrencyGetter = new RealCurrencyGetter();
        }

        public ObservableCollection<Currency> Currencies
        {
            get
            {
                if(storage.LastCurrencyUpdateDate == storage.LastActiveDate)
                {
                    return storage.CurrenciesCash;
                }
                else
                {
                    try
                    {
                        ObservableCollection<Currency> newCurrencyData = realCurrencyGetter.Currencies;
                        Currency currency;
                        foreach(var newCurrency in newCurrencyData)
                        {
                            currency = storage.CurrenciesCash.FirstOrDefault(c => c.Code == newCurrency.Code);
                            if(currency == null)
                            {
                                storage.AddCurrency(newCurrency);
                            }
                            else
                            {
                                storage.UpdateCurrency(currency, newCurrency);
                            }
                        }

                        if((currency = storage.CurrenciesCash.FirstOrDefault(c => c.Code == "AZN")) == null)
                        {
                            storage.AddCurrency(new Currency { Code = "AZN", Value = 1 });
                        }
                        storage.Save();
                        storage.LastCurrencyUpdateDate = DateTime.Now.Date;
                    }
                    catch (Exception ex) { 
                        if(storage.CurrenciesCash.Count == 0)
                        {
                            throw ex;
                        }
                    }
                    return storage.CurrenciesCash;
                }
            }
        }
    }
}
