using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using Monify.Models;
using System.IO;

namespace Monify.Services
{
    class JsonFileSaveLoader : IFileSaveLoader
    {
        IStorage storage;

        public JsonFileSaveLoader(IStorage storage, string saveFileLocation = null)
        {
            this.storage = storage;
            SaveFileLocation = saveFileLocation ?? "SaveFile.json";
        }

        public string SaveFileLocation { get; set; }

       

        public void Load()
        {

            var loadedData = JsonConvert.DeserializeObject<Tuple<ObservableCollection<Account>, ObservableCollection<Currency>, ObservableCollection<Operation>, ObservableCollection<OperationCategory>, ObservableCollection<OperationType>>>(File.ReadAllText(SaveFileLocation));
            storage.Accounts = loadedData.Item1;
            storage.Currencies = loadedData.Item2;
            storage.Operations = loadedData.Item3;
            storage.OperationCategories = loadedData.Item4;
            storage.OperationTypes = loadedData.Item5;

        }

        public void Save()
        {
            var savedData = new Tuple<ObservableCollection<Account>, ObservableCollection<Currency>, ObservableCollection<Operation>, ObservableCollection<OperationCategory>, ObservableCollection<OperationType>>(storage.Accounts, storage.Currencies, storage.Operations, storage.OperationCategories, storage.OperationTypes);
            
            File.WriteAllText(SaveFileLocation, JsonConvert.SerializeObject(savedData));
            
        }

        public void EraseSave()
        {
            File.Delete(SaveFileLocation);
        }
    }
}
