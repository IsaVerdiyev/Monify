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
using MyMonify.Models;
using MyMonify.Services.CurrencyGetterService;
using MyMonify.Services.TranslatorService;
using MyMonify.Tools;

namespace MyMonify.Services
{
    class FileDataStorage :ObservableObject, IStorage
    {
        static FileDataStorage storage;

        ITranslator translator;
        ICurrencyGetter currencyGetter;

        public IFileSaveLoader FileSaveLoader { get; set; }

        ObservableCollection<OperationCategory> operationCategories;
        ObservableCollection<OperationType> operationTypes;
        ObservableCollection<Operation> operations;
        ObservableCollection<Account> accounts;
        ObservableCollection<Currency> currencies;
        DateTime? lastActiveDate;
        DateTime? lastCurrencyUpdateDate;

        ObservableCollection<Translation> translations;
        ObservableCollection<AppString> appStrings;
        ObservableCollection<Language> languages;
        Language chosenLanguage;

        

        private FileDataStorage()
        {
            currencyGetter = new ProxyCurrencyGetter(this);
            translator = new TranslatorProxy(this);
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
        public ObservableCollection<Translation> TranslationCash { get => translations; set => translations = value; }
        public ObservableCollection<AppString> AppStrings { get => appStrings; set => appStrings = value; }
        public ObservableCollection<Language> Languages { get => languages; set => languages = value; }
        public Language SelectedLanguage {
            get {
                if (chosenLanguage == null)
                {
                    chosenLanguage = Languages.FirstOrDefault(l => l.Code == "en");
                   
                }
                return chosenLanguage;

            }
            set
            {
                chosenLanguage = value;
            }
        }

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

            TranslationCash = new ObservableCollection<Translation>();

            Languages = new ObservableCollection<Language>();

            AppStrings = new ObservableCollection<AppString>();

            InitializeLanguages();
           


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

        void InitializeLanguages()
        {
            IList<Tuple<string, string>> tuples = translator.GetAvailableLanguages();

            foreach (var tuple in tuples)
            {
                languages.Add(new Language { FullName = tuple.Item1, Code = tuple.Item2 });
            }
            Save();
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
            TranslationCash.Add(translation);
            Save();
        }

        public void AddAppString(AppString appString)
        {
            AppStrings.Add(appString);
            Save();
        }

        public string GetTranslation(string key)
        {
            return translator.Translate(key);
        }
    }
}
