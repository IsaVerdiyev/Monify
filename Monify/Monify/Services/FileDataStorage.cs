using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monify.Models;
using Monify.Tools;

namespace Monify.Services
{
    class FileDataStorage :ObservableObject, IStorage
    {
        static FileDataStorage storage;

        ObservableCollection<OperationCategory> operationCategories;
        ObservableCollection<OperationType> operationTypes;
        ObservableCollection<Operation> operations;
        ObservableCollection<IAccount> accounts;
        ObservableCollection<AccountType> accountTypes;

        private FileDataStorage()
        {
            Initialize();
        }

        public static FileDataStorage Storage { get => storage ?? (storage = new FileDataStorage()); }
       
        public ObservableCollection<IAccount> Accounts { get => accounts; set => SetProperty(ref accounts,value); }
        public ObservableCollection<AccountType> AccountTypes { get => accountTypes; set => SetProperty(ref accountTypes, value); }
        public ObservableCollection<OperationType> OperationTypes { get => operationTypes; set => SetProperty(ref operationTypes, value); }
        public ObservableCollection<OperationCategory> OperationCategories { get => operationCategories; set => SetProperty(ref operationCategories, value); }
        public ObservableCollection<Operation> Operations { get => operations; set => SetProperty(ref operations, value); }

       
        public void Initialize()
        {
            OperationTypes = new ObservableCollection<OperationType>
            {
                new OperationType{Name = "Expense"} ,
                new OperationType{Name = "Profit"}
            };

            OperationCategories = new ObservableCollection<OperationCategory>
            {
                new OperationCategory{
                    Name = "Hygiene",
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
                },

                new OperationCategory{
                    Name = "Health",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Cafe",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Car",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Clothes",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Pets",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Presents",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Entertainments",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Communication",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Sports",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Taxi",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Transport",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Expense").Index
                },
                new OperationCategory{
                    Name = "Deposits",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Profit").Index
                },
                new OperationCategory{
                    Name = "Salary",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Profit").Index
                },
                new OperationCategory{
                    Name = "Saving",
                    OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == "Profit").Index
                }
            };
            AccountTypes = new ObservableCollection<AccountType>
            {
                new AccountType{Name = "Cash"},
                new AccountType{Name = "Payment Card"}
            };

            Accounts = new ObservableCollection<IAccount>();

            Operations = new ObservableCollection<Operation>();


        }
    }
}
