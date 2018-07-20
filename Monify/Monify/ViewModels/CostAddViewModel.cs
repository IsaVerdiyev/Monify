﻿using Monify.Services;
using Monify.Services.CalculatorService;
using Monify.Tools;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Monify.ViewModels
{
    class CostAddViewModel: AbstractCalculatorViewModel
    {
        public IStorage Storage { get; }

        public CostAddViewModel()
        {
            Storage = StorageGetter.Storage;
        }

        UserControl currentControl;

        public UserControl CurrentControl { get => currentControl; set => SetProperty(ref currentControl, value); }

        public DateTime CurrentDate { get => DateTime.Now; }

        RelayCommand returnToMainViewCommand;

        public RelayCommand ReturnToMainViewCommand
        {
            get
            {
                return returnToMainViewCommand ??
                    (returnToMainViewCommand = new RelayCommand(obj =>
                    {
                        ((WindowViewModel)(ViewModelsStorage.ViewModels[VM.WindowViewModel])).CurrentControl = new MainView();
                    }));
            }
        }

        public override IViewModel ResetToInitialState()
        {
            CurrentControl = new CostProfitCalculatorView();
            CalculatorState = new InitialCalculatorState(this);
            CalculatorHistory = null;
            TextBoxNumber = "";
            return this;
        }

        
    }
}
