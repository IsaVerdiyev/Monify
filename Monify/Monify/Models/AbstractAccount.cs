
using Monify.Services;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    abstract class AbstractAccount : ObservableObject, IAccount
    {
        static int iterator = 0;
        int index;
        int accountTypeIndex;
        int? currencyIndex;
        protected double balance;
        string name;
        string imagePath;

        public AbstractAccount()
        {
            index = iterator++;
        }

        public int Index { get => index; }

        public abstract double Balance { get; set; }

        public int AccountTypeIndex
        {
            get => accountTypeIndex;
            set => accountTypeIndex = value;
        }


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
                    Balance = CurrencyConverter.Convert(currencyIndex.Value, value.Value, Balance);
                
                SetProperty(ref currencyIndex, value);

            }
        }

        
    }
}
