using MyMonify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonify.Tools
{
    static class StorageGetter
    {
        static IStorage storage;
        public static IStorage Storage
        {
            get
            {
                return storage ?? (storage = DatabaseStorage.Storage);
            }
        }
    }
}
