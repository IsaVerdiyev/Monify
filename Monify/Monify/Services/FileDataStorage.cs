using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.Models;

namespace Monify.Services
{
    class FileDataStorage : IStorage
    {
        static FileDataStorage storage;

        ObservableCollection<OperationCategory> operationCategories;
        ObservableCollection<OperationType> operationTypes;
        ObservableCollection<Operation> operations;
        ObservableCollection<Account> accounts;
        ObservableCollection<AccountType> accountTypes;

        private FileDataStorage()
        {
            operationTypes = new ObservableCollection<OperationType>
            {
                new OperationType{Name = "Expense"} ,
                new OperationType{Name = "Profit"}
            };

            operationCategories = new ObservableCollection<OperationCategory>
            {
                new OperationCategory{
                    Name = "hygiene",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory
                {
                    Name = "Food",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                  new OperationCategory{
                    Name = "Accommodation",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Health",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Cafe",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Car",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Clothes",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Pets",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Presents",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Entertainments",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }, new OperationCategory{
                    Name = "Communication",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                }
            };
        }

        public static FileDataStorage Storage { get => storage ?? (storage = new FileDataStorage()); }
       
        public ObservableCollection<Account> Accounts { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<AccountType> AccountTypes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<OperationType> OperationTypes { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<OperationCategory> OperationCategories { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Operation> Operations { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public ObservableCollection<>
    }
}
