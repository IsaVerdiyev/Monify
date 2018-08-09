using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Monify.Models;
using Monify.Services.CurrencyGetterService;
using Monify.Tools;

namespace Monify.Services
{
    class FileDataStorage :ObservableObject, IStorage
    {
        static FileDataStorage storage;

        ICurrencyGetter currencyGetter;

        public IFileSaveLoader FileSaveLoader { get; set; }

        ObservableCollection<OperationCategory> operationCategories;
        ObservableCollection<OperationType> operationTypes;
        ObservableCollection<Operation> operations;
        ObservableCollection<Account> accounts;
        ObservableCollection<Currency> currencies;
        DateTime? lastActiveDate;
        DateTime? lastCurrencyUpdateDate;
        

        

        private FileDataStorage()
        {
            currencyGetter = new ProxyCurrencyGetter(this);
            FileSaveLoader = new JsonFileSaveLoader(this);
            try
            {
                Load();
            }
            catch(FileNotFoundException ex)
            {
                Initialize();
            }
        }

        public static FileDataStorage Storage { get => storage ?? (storage = new FileDataStorage()); }
       
        public ObservableCollection<Account> Accounts { get => accounts; set => SetProperty(ref accounts,value); }
        public ObservableCollection<OperationType> OperationTypes { get => operationTypes; set => SetProperty(ref operationTypes, value); }
        public ObservableCollection<OperationCategory> OperationCategories { get => operationCategories; set => SetProperty(ref operationCategories, value); }
        public ObservableCollection<Operation> Operations { get => operations; set => SetProperty(ref operations, value); }

        public ObservableCollection<Currency> Currencies { get => currencyGetter.Currencies; set => SetProperty(ref currencies, value); }
        public ObservableCollection<Currency> CurrenciesCash { get => currencies; set => currencies = value; }

        public DateTime? LastActiveDate {
            get
            {
                if(lastActiveDate > DateTime.Now.Date)
                {
                    throw new Exception("Incompatibility error of last active time of app and current time");
                }
                return (lastActiveDate = DateTime.Now.Date);
            }

            set => lastActiveDate = value;
        }
        public DateTime? LastCurrencyUpdateDate {
            get => lastCurrencyUpdateDate;
            set => lastCurrencyUpdateDate = value;
        }
        public ObservableCollection<Translation> TranslationCash { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<AppString> AppStrings { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ObservableCollection<Language> Languages { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Language SelectedLanguage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Initialize()
        {
            OperationTypes = new ObservableCollection<OperationType>
            {
                new OperationType{Name = OperationTypesEnum.Profit.ToString()} ,
                new OperationType{Name = OperationTypesEnum.Expense.ToString()}
            };

            OperationCategories = new ObservableCollection<OperationCategory>();

            InitializeOperationCategories();

            Accounts = new ObservableCollection<Account>();

            Operations = new ObservableCollection<Operation>();

            Currencies = new ObservableCollection<Currency>();

           


        }







        void RetryInitialize()
        {
            Accounts.Clear();
            Operations.Clear();

            OperationCategories.Clear();
            InitializeOperationCategories();

        }

        void InitializeOperationCategories()
        {
            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Hygiene.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Food.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Accomodation.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Health.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Cafe.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Car.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Clothes.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Pets.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Presents.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Entertainments.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Communication.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Sports.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Taxi.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Transport.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Transaction.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id
            });



            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Deposits.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Salary.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Saving.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id
            });

            OperationCategories.Add(new OperationCategory
            {
                Name = OperationCategoryEnum.Transaction.ToString(),
                OperationTypeIndex = operationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id
            });
        }


      

        public void Save()
        {
            try
            {
                FileSaveLoader.Save();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Load()
        {
            try
            {
                FileSaveLoader.Load();
            }catch(FileNotFoundException ex)
            {
                throw ex;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void EraseData()
        {
            try
            {
                FileSaveLoader.EraseSave();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            storage.RetryInitialize();
        }

        public void AddAccount(Account account)
        {
            Accounts.Add(account);
            Save();
        }

        public void AddOperationCategory(OperationCategory category)
        {
            OperationCategories.Add(category);
            Save();
        }

        public void AddOperation(Operation operation)
        {
            Operations.Add(operation);
            Save();
        }

        public void AddCurrency(Currency currency)
        {
            CurrenciesCash.Add(currency);
        }

        public void UpdateCurrency(Currency currency, Currency newCurrency)
        {
            currency.Value = newCurrency.Value;
        }

        public void AddTranslation(Translation translation)
        {
            throw new NotImplementedException();
        }

        public void AddAppString(AppString appString)
        {
            throw new NotImplementedException();
        }
    }
}
