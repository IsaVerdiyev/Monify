﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Services
{
    interface ISaveLoader
    {
        void Save();
        void Load();
        void EraseSave();
    }
}
