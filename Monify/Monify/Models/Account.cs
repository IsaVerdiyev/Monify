﻿using Monify.Services;
using Monify.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Models
{
    class Account: ObservableObject
    {
        int iterator = 0;
        int index;
        double balance;
        int accountTypeIndex;
        string name;
        string imagePath;

        public Account()
        {
            index = iterator++;
        }

        public int Index { get => index; }

        public double Balance {
            get => balance;
            set => SetProperty(ref balance, value);
        }

        public int AccountTypeIndex {
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


    }
}
