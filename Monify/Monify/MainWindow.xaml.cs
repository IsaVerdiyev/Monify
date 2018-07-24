using Monify.Tools;
using Monify.ViewModels;
using Monify.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Monify
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            DataContext = (WindowViewModel)ViewModelsStorage.Add(typeof(WindowViewModel).Name, $"{typeof(WindowViewModel).Namespace}.{typeof(WindowViewModel).Name}");
            ((WindowViewModel)(DataContext)).CurrentControl = new MainView();

            //mainView = new MainView(this);
            //MainGrid.Children.Add(mainView);
        }
    }
}
