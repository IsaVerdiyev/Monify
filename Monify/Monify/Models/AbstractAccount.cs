
using Monify.Services;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    abstract class AbstractAccount : ObservableObject
    {
        static int iterator = 0;
        protected int index;
        int? currencyIndex;
        string name;
        string imagePath;

        protected double? balance;

        
       
       
        

        string icon;

        public string Icon { get => icon; set => SetProperty(ref icon, value); }

        public AbstractAccount()
        {
            index = iterator++;
        }

        public int Index { get => index; }

        public abstract double? Balance { get; set; }


        public string Name
        {
            get => name;
            set => name = value;
        }

        public string ImagePath
        {
            get => imagePath;
            set => SetProperty(ref imagePath, value);
        }


        public int? CurrencyIndex {
            get => currencyIndex;
            set
            {
                if(currencyIndex != null)
                    Balance = CurrencyConverter.Convert(currencyIndex.Value, value.Value, Balance.Value);
                
                SetProperty(ref currencyIndex, value);

            }
        }

        
        public override string ToString()
        {
            return Name;
        }

    }
}
