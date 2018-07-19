using Monify.Tools;
using Monify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Monify.Views
{
    /// <summary>
    /// Interaction logic for CostAddCategoriesView.xaml
    /// </summary>
    public partial class CostAddCategoriesView : UserControl
    {
        internal CostAddCategoriesView()
        {
            InitializeComponent();
            DataContext = (CostAddCategoriesViewModel)ViewModelsStorage.ViewModels[VM.CostAddCategoriesViewModel].ResetToInitialState();
        }
    }
}
