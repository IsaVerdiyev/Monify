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
                    return storage.CurrencyCollectionFromDbSet;
                }
                else
                {
                    try
                    {
                        ObservableCollection<Currency> newCurrencyData = realCurrencyGetter.Currencies;
                        storage.EraseCurrencies();
                        storage.AddCurrencies(newCurrencyData);
                        storage.LastCurrencyUpdateDate = DateTime.Now.Date;
                    }
                    catch (Exception ex) { 
                        if(storage.CurrencyCollectionFromDbSet.Count == 0)
                        {
                            throw ex;
                        }
                    }
                    return storage.CurrencyCollectionFromDbSet;
                }
            }
        }
    }
}
