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

namespace Monify.Services
{
    class DatabaseStorage : DbContext, IStorage
    {
        public static DatabaseStorage storage;

        public DbSet<Account> accounts { get; set; }
        public DbSet<OperationCategory> operationCategories { get; set; }
        public DbSet<Operation> operations { get; set; }
        public DbSet<OperationType> operationTypes { get; set; }
        public DbSet<Currency> currencies { get; set; }

        private DatabaseStorage(): base("DefaultConnection")
        {
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

        public ObservableCollection<Account> Accounts
        {
            get
            {
                accounts.Load();
                return accounts.Local;
            }
            set { }
        }
        public ObservableCollection<OperationType> OperationTypes
        {
            get
            {
                operationTypes.Load();
                return operationTypes.Local;
            }
            set { }
        }
        public ObservableCollection<OperationCategory> OperationCategories
        {
            get
            {
                operationCategories.Load();
                return operationCategories.Local;
            }
            set { }
        }
        public ObservableCollection<Operation> Operations {
            get
            {
                operations.Load();
                return operations.Local;
            }
            set { }
        }
        public ObservableCollection<Currency> Currencies
        {
            get
            {
                currencies.Load();
                return currencies.Local;
            }
            set { }
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

        void AddCurrency(Currency currency)
        {
            currencies.Add(currency);
            SaveChanges();
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
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            
            InitializeOperationCategories();

            InitializeCurrencies();
            
        }

        void InitializeOperationCategories()
        {
            SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = $"INSERT INTO {nameof(operationCategories)}({nameof(OperationCategory.Name)}, {nameof(OperationCategory.OperationTypeIndex)}) VALUES " +
                $"('{OperationCategoryEnum.Accomodation.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Cafe.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Car.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Clothes.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Communication.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Entertainments.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Food.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Health.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Hygiene.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Pets.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Presents.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Sports.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Taxi.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Transport.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Transaction.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Deposits.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Salary.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Saving.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}'), " +
                $"('{OperationCategoryEnum.Transaction.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}')";
                command.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        string GetXmlOfCurrencies()
        {
            using (WebClient web = new WebClient())
            {
                string day = String.Format("{0,2}", DateTime.Now.Day.ToString()).Replace(' ', '0');
                string month = String.Format("{0,2}", DateTime.Now.Month.ToString()).Replace(' ', '0');
                string date = day + "." + month + "." + DateTime.Now.Year;
                string unformattedUrl = "http://www.cbar.az/currencies/" + date + ".xml";
                string url = string.Format(unformattedUrl);
                string data = web.DownloadString(url);
                return data;

            }
        }

        public void InitializeCurrencies()
        {
            string xmlData = GetXmlOfCurrencies();

            var doc = new XmlDocument();
            doc.LoadXml(xmlData);



            var nodes = doc.SelectNodes("/ValCurs/ValType[@Type='Xarici valyutalar']/Valute");
            foreach (XmlNode item in nodes)
            {
                Currency currency = new Currency
                {
                    Code = item.Attributes["Code"].InnerText.ToUpper(),
                    Value = Double.Parse(item["Value"].InnerText)
                };


                Currency searchedCurrency = Currencies.FirstOrDefault(c => c.Code == currency.Code);
                if (searchedCurrency != null)
                {
                    searchedCurrency.Value = currency.Value;
                }
                else
                {
                    AddCurrency(currency);
                }
            }

            if (Currencies.FirstOrDefault(c => c.Code == "AZN") == null)
            {
                Currency aznCurrency = new Currency
                {
                    Code = "AZN",
                    Value = 1
                };

                AddCurrency(aznCurrency);
            }
        }

        public void Load()
        {
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
