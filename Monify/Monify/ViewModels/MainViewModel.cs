using Monify.Services;
using Monify.Tools;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Monify.ViewModels
{
    class MainViewModel: ObservableObject
    {
        public IStorage Storage { get; }

        MainWindow window;

        public MainViewModel(Window window)
        {
            Storage = StorageGetter.Storage;
            this.window = (MainWindow)window;
        }

        public DateTime CurrentDate { get => DateTime.Now; }

        public DateTime Yesterday { get => CurrentDate.AddDays(-1); }

        public DayOfWeek dayOfWeek { get => CurrentDate.DayOfWeek; }

        RelayCommand addCost;

        public RelayCommand AddCost {
            get
            {
                return addCost ??
                    (addCost = new RelayCommand(obj =>
                    {
                        window.MainGrid.Children.Clear();
                        window.MainGrid.Children.Add(new CostAddView());
                    }
                    ));
            }
        }

        RelayCommand popUpStatisticsVisibilitySettings;

        public RelayCommand PopUpStatisticsVisibilitySettings
        {
            get
            {
                return popUpStatisticsVisibilitySettings ??
                    (popUpStatisticsVisibilitySettings = new RelayCommand(obj =>
                    {
                        ((MainView)(window.MainGrid.Children[0])).VisibilitySettingsStackPanel.Margin = new Thickness(0, 0, 0, 0);
                    }));
            }
        }
    }
}
