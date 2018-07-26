
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
    class Account : ObservableObject
    {
        static int iterator = 0;
        protected int index;
        int? currencyIndex;
        string name;
        string imagePath;
        DateTime? startDate;

        protected double? balance;

        string icon;

        public string Icon { get => icon; set => SetProperty(ref icon, value); }

        public Account()
        {
            index = iterator++;
        }

        public Account(int index)
        {
            this.index = index;
        }

        public int Index { get => index; }

        public double? Balance { get => balance; set => SetProperty(ref balance, value); }


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

        public DateTime? StartDate { get => startDate; set => SetProperty(ref startDate, value); }
        
        public override string ToString()
        {
            return Name;
        }

    }
}
