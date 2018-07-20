using Monify.AbstractClassesAndInterfaces.AbstractClassesAndInterfaces.ViewModels;
using Monify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{

    static class ViewModelsStorage
    {
        static Dictionary<string, IViewModel> viewModels;


        static Dictionary<string, IViewModel> ViewModels {
            get {
                return viewModels ??
                    (viewModels = new Dictionary<string, IViewModel>());
            }
        }

        public static IViewModel Add(string key, string fullname)
        {
            if (!ViewModels.ContainsKey(key))
            {
                Type type = Type.GetType(fullname, true);
                IViewModel viewModel = Activator.CreateInstance(type) as IViewModel;
                ViewModels.Add(key, viewModel);
               
            }
            else
            {
                ViewModels[key].ResetToInitialState();
            }
            return ViewModels[key];
        }

        public static IViewModel GetViewModel(string key)
        {
            return ViewModels[key];
        }
    }
}
