﻿using Monify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    enum VM{
        WindowViewModel,
        MainViewModel
    }

    static class ViewModelsStorage
    {
        static Dictionary<VM, IViewModel> viewModels;


        public static Dictionary<VM, IViewModel> ViewModels {
            get {
                return viewModels ??
                    (viewModels = new Dictionary<VM, IViewModel>()
                    {
                        {VM.WindowViewModel, new WindowViewModel() },
                        {VM.MainViewModel, new MainViewModel() }
                    }

                    );
            } }
    }
}
