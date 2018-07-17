using Monify.Models;
using Monify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monify.Tools
{
    class AllUsers : IAccount
    {
        IStorage storage;
        public string Name { get; set ; }

        public AllUsers()
        {
            storage = StorageGetter.Storage;
            Name = "All Users";
        }

        public double Balance => storage.Accounts.Sum(a => a.Balance);
    }
}
