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
    class JsonSaveLoader : ISaveLoader
    {
        IStorage storage;

        public JsonSaveLoader(IStorage storage)
        {
            this.storage = storage;
        }

        public void Load()
        {
            var openFileDialogue = new OpenFileDialog();
            openFileDialogue.Filter = "json files | *.json";
            if (openFileDialogue.ShowDialog() == true)
            {

                storage.Expenses = JsonConvert.DeserializeObject<ObservableCollection<Expense>>(File.ReadAllText(openFileDialogue.FileName));
                
            }
        }

        public void Save()
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = "savefile.json";
            saveFileDialog.Filter = "json files | *.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                string saveContents = JsonConvert.SerializeObject(storage.Expenses);
                File.WriteAllText(saveFileDialog.FileName, saveContents);
            }
        }
    }
}
