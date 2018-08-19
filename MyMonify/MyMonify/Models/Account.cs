
using MyMonify.Services;
using MyMonify.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Models
{
    class Account : ObservableObject
    {
        static int iterator = 0;
        protected int id;
        int? currencyIndex;
        string name;
        string imagePath;
        DateTime? startDate;

        protected double? balance;

        string icon;

        public string Icon { get => icon; set => SetProperty(ref icon, value); }

        public Account()
        {
            id = iterator++;
        }

        public Account(int index)
        {
            this.id = index;
        }

        public int Id { get => id; private set => id = value; }

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
                if (currencyIndex != null)
                    if (Balance != null)
                    {
                        Balance = CurrencyConverter.Convert(currencyIndex.Value, value.Value, Balance.Value);
                    }
                
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
