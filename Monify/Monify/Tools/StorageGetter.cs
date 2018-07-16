using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    static class StorageGetter
    {
        static IStorage storage;
        public static IStorage Storage
        {
            get
            {
                return storage ?? (storage = FileDataStorage.Storage);
            }
        }
    }
}
