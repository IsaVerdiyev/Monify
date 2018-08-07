using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Monify.Models;
using Monify.Services.CurrencyGetterService;

namespace Monify.Services
{
    class DatabaseStorage : DbContext, IStorage
    {


        ICurrencyGetter currencyGetter;
        public static DatabaseStorage storage;

        public DbSet<Account> accounts { get; set; }
        public DbSet<OperationCategory> operationCategories { get; set; }
        public DbSet<Operation> operations { get; set; }
        public DbSet<OperationType> operationTypes { get; set; }
        public DbSet<Currency> currencies { get; set; }
        public DbSet<AppDate> appDates { get; set; }
        public DbSet<AppString> appStrings { get; set; }
        public DbSet<Language> languages { get; set; }
        public DbSet<Translation> translations { get; set; }
        
        

        private DatabaseStorage(): base("DefaultConnection")
        {
            currencyGetter = new ProxyCurrencyGetter(this);

            if (File.Exists("Monify.db"))
            {
                Load();
            }
            else
            {
                Initialize();
            }
        }

        public static DatabaseStorage Storage { get => storage ?? (storage = new DatabaseStorage()); }

        public ObservableCollection<Account> Accounts { get => accounts.Local; set { } }
        public ObservableCollection<OperationType> OperationTypes { get => operationTypes.Local; set { } }
        public ObservableCollection<OperationCategory> OperationCategories { get => operationCategories.Local; set { } }
        public ObservableCollection<Operation> Operations { get => operations.Local; set { } }
       
        

        public ObservableCollection<Currency> CurrenciesCash { get => currencies.Local; set => throw new Exception("Cannot use setter of CurrenciesCash in DatabaseStorage"); }
        public ObservableCollection<Currency> Currencies { get => currencyGetter.Currencies; set { } }

        

        public DateTime? LastActiveDate {
            get
            {
                if(appDates.Local.FirstOrDefault(d => d.Name == AppDateEnum.LastActiveDate.ToString()).Date > DateTime.Now.Date)
                {
                    throw new Exception("Incompatibility error of last active time of app and current time");
                }
                appDates.Local.FirstOrDefault(d => d.Name == AppDateEnum.LastActiveDate.ToString()).Date = DateTime.Now.Date;
                SaveChanges();
                return appDates.Local.FirstOrDefault(d => d.Name == AppDateEnum.LastActiveDate.ToString()).Date;
            }
            set => throw new Exception("Cannot use setter LastActiveDate of DatabaseStorage");
        }
        public DateTime? LastCurrencyUpdateDate {
            get => appDates.Local.FirstOrDefault(d => d.Name == AppDateEnum.LastCurrencyUpdateDate.ToString()).Date;
            set
            {
                appDates.Local.FirstOrDefault(d => d.Name == AppDateEnum.LastCurrencyUpdateDate.ToString()).Date = value;
                SaveChanges();
            }
        }

        

        public void AddAccount(Account account)
        {
            accounts.Add(account);
            SaveChanges();
        }

        public void AddOperation(Operation operation)
        {
            operations.Add(operation);
            SaveChanges();
        }

        public void AddOperationCategory(OperationCategory category)
        {
            operationCategories.Add(category);
            SaveChanges();
        }

        public void AddOperationCategory(params OperationCategory[] categories)
        {
            if(categories.Count() == 0)
            {
                return;
            }

            operationCategories.AddRange(categories);
            SaveChanges();
        }

        public void AddCurrency(Currency currency)
        {
            currencies.Add(currency);
        }

        public void UpdateCurrency(Currency currency, Currency newCurrency) {
            currency.Value = newCurrency.Value;
        }



        public void EraseData()
        {
            accounts.RemoveRange(accounts);
            operationCategories.RemoveRange(operationCategories);
            operations.RemoveRange(operations);
            Save();

            InitializeOperationCategories();
        }

        public void Initialize()
        {
            SQLiteConnection.CreateFile("Monify.db");
            SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try { 
            connection.Open();
            
                string sqlString = $"CREATE TABLE {nameof(currencies)}  (" +
                    $"{nameof(Currency.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(Currency.Code)} NVARCHAR(10), " +
                    $"{nameof(Currency.Value)} FLOAT)";
                SQLiteCommand command = new SQLiteCommand(sqlString, connection);
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(operationTypes)} (" +
                    $"{nameof(OperationType.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(OperationType.Name)} NVARCHAR(30))"; 
                command.ExecuteNonQuery();

                command.CommandText = $"INSERT INTO {nameof(operationTypes)}({nameof(OperationType.Name)}) VALUES" +
                    $"('{OperationTypesEnum.Expense.ToString()}'), " +
                    $"('{OperationTypesEnum.Profit.ToString()}')";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(operationCategories)} (" +
                    $"{nameof(OperationCategory.Id)} INTEGER  PRIMARY KEY, " +
                    $"{nameof(OperationCategory.Name)} NVARCHAR(30), " +
                    $"{nameof(OperationCategory.OperationTypeIndex)} INTEGER, " +
                    $"FOREIGN KEY({nameof(OperationCategory.OperationTypeIndex)}) REFERENCES {nameof(operationTypes)}({nameof(OperationType.Id)}))";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(accounts)}(" +
                    $"{nameof(Account.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(Account.Icon)} NVARCHAR(4), " +
                    $"{nameof(Account.ImagePath)} NVARCHAR(100), " +
                    $"{nameof(Account.Balance)} FLOAT, " +
                    $"{nameof(Account.Name)} NVARCHAR(30), " +
                    $"{nameof(Account.CurrencyIndex)} INTEGER, " +
                    $"{nameof(Account.StartDate)} DATETIME, " +
                    $"FOREIGN KEY({nameof(Account.CurrencyIndex)}) REFERENCES {nameof(currencies)}({nameof(Currency.Id)}))";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(operations)}(" +
                    $"{nameof(Operation.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(Operation.OperationCategoryIndex)} INTEGER, " +
                    $"{nameof(Operation.Amount)} FLOAT, " +
                    $"{nameof(Operation.Date)} DATETIME, " +
                    $"{nameof(Operation.AccountIndex)} INTEGER, " +
                    $"FOREIGN KEY({nameof(Operation.OperationCategoryIndex)}) REFERENCES {nameof(operationCategories)}({nameof(OperationCategory.Id)}), " +
                    $"FOREIGN KEY({nameof(Operation.AccountIndex)}) REFERENCES {nameof(accounts)}({nameof(Account.Id)}))";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(appDates)}(" +
                    $"{nameof(AppDate.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(AppDate.Name)} NVARCHAR(30), " +
                    $"{nameof(AppDate.Date)} DATETIME)";
                command.ExecuteNonQuery();

                command.CommandText = $"INSERT INTO {nameof(appDates)}({nameof(AppDate.Name)}) VALUES " +
                    $"('{AppDateEnum.LastCurrencyUpdateDate.ToString()}'), " +
                    $"('{AppDateEnum.LastActiveDate.ToString()}')";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(appStrings)}(" +
                    $"{nameof(AppString.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(AppString.Word)} NVARCHAR(30))";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(languages)}(" +
                    $"{nameof(Language.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(Language.FullName)} NVARCHAR(30), " +
                    $"{nameof(Language.Code)} NVARCHAR(5))";
                command.ExecuteNonQuery();

                command.CommandText = $"CREATE TABLE {nameof(translations)}(" +
                    $"{nameof(Translation.Id)} INTEGER PRIMARY KEY, " +
                    $"{nameof(Translation.Id_Lang)} INTEGER, " +
                    $"{nameof(Translation.Id_Word)} INTEGER, " +
                    $"{nameof(Translation.Result)} NVARCHAR(30), " +
                    $"FOREIGN KEY({nameof(Translation.Id_Lang)}) REFERENCES {nameof(languages)}({nameof(Language.Id)}), " +
                    $"FOREIGN KEY({nameof(Translation.Id_Word)}) REFERENCES {nameof(appStrings)}({nameof(AppString.Id)}))";
                command.ExecuteNonQuery();



                Load();
                InitializeOperationCategories();
                InitializeAppStrings();
                
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            

            
        }

        void InitializeAppStrings()
        {
            List<string> list = Enum.GetValues(typeof(AppStringEnum)).Cast<string>().ToList();
            foreach(string word in list)
            {
                appStrings.Add(new AppString { Word = word });
            }
            list = Enum.GetValues(typeof(OperationCategoryEnum)).Cast<string>().ToList();
            foreach(string word in list)
            {
                appStrings.Add(new AppString { Word = word});
            }
            list = Enum.GetValues(typeof(OperationTypesEnum)).Cast<string>().ToList();
            foreach(string word in list)
            {
                appStrings.Add(new AppString { Word = word });
            }
            Save();
        }

        void InitializeOperationCategories()
        {
            try { 
                AddOperationCategory(
                    new OperationCategory {Name = OperationCategoryEnum.Accomodation.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Cafe.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Car.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Clothes.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Communication.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Entertainments.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Food.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Health.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Hygiene.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Pets.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Presents.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Sports.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Taxi.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Transport.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Transaction.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Deposits.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Salary.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Saving.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id },
                    new OperationCategory { Name = OperationCategoryEnum.Transaction.ToString(), OperationTypeIndex = OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id }
                    );
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

       

        public void Load()
        {
            appDates.Load();
            accounts.Load();
            currencies.Load();
            operations.Load();
            operationTypes.Load();
            operationCategories.Load();
        }

        public void Save()
        {
            SaveChanges();
        }

       
       

        

        
    }
}
