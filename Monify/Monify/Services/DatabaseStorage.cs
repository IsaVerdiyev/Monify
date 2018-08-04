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
using System.Xml;
using Monify.Models;

namespace Monify.Services
{
    class DatabaseStorage : DbContext, IStorage
    {
        public static DatabaseStorage storage;

        public DbSet<Account> AccountTable { get; set; }
        public DbSet<OperationCategory> OperationCategoryTable { get; set; }
        public DbSet<Operation> OperationTable { get; set; }
        public DbSet<OperationType> OperationTypeTable { get; set; }
        public DbSet<Currency> CurrencyTable { get; set; }

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

        public ObservableCollection<Account> Accounts { get => AccountTable.Local; set { } }
        public ObservableCollection<OperationType> OperationTypes { get => OperationTypeTable.Local; set { } }
        public ObservableCollection<OperationCategory> OperationCategories { get => OperationCategoryTable.Local; set { } }
        public ObservableCollection<Operation> Operations { get => OperationTable.Local; set { } }
        public ObservableCollection<Currency> Currencies { get => CurrencyTable.Local; set { } }

        public void AddAccount(Account account)
        {
            AccountTable.Add(account);
            SaveChanges();
        }

        public void AddOperation(Operation operation)
        {
            OperationTable.Add(operation);
            SaveChanges();
        }

        public void AddOperationCategory(OperationCategory category)
        {
            OperationCategoryTable.Add(category);
            SaveChanges();
        }

        

        public void EraseData()
        {
            using(SQLiteConnection connection = new SQLiteConnection())
            {
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = "DELETE FROM AccountTable";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM OperationTable";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM OperationCategoryTable";
                command.ExecuteNonQuery();
            }

            InitializeOperationCategories();
        }

        public void Initialize()
        {
            SQLiteConnection.CreateFile("Monify.db");
            using(SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string sqlString = "CREATE TABLE CurrencyTable (" +
                    "Index INTEGER PRIMARY KEY," +
                    " Code NVARCHAR(10)," +
                    " Value FLOAT)";
                SQLiteCommand command = new SQLiteCommand(sqlString, connection);
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE OperationTypeTable (" +
                    "Index INTEGER PRIMARY KEY," +
                    " Name NVARCHAR(30))"; 
                command.ExecuteNonQuery();

                command.CommandText = $"INSERT INTO OperationTypeTable(Name) VALUES({OperationTypesEnum.Expense.ToString()}, )";
                command.ExecuteNonQuery();

                command.CommandText = $"INSERT INTO OperationTypeTable(Name) VALUES({OperationTypesEnum.Profit.ToString()})";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE OperationCategoryTable (" +
                    "Index INTEGER  PRIMARY KEY," +
                    " Name NVARCHAR(30)," +
                    " OperationTypeIndex INTEGER," +
                    " FOREIGN KEY(OperationTypeIndex) REFERENCES OperationTypeTable(Index))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE AccountTable (" +
                    "Index INTEGER PRIMARY KEY," +
                    " Name NVARCHAR(30)," +
                    " CurrencyIndex INTEGER," +
                    " StartDate TEXT," +
                    " FOREIGN KEY(CurrencyIndex) REFERENCES CurrencyTable(INDEX)";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE OperationTable (" +
                    "Index INTEGER PRIMARY KEY, " +
                    "OperationCategoryIndex INTEGER, " +
                    "Amount FLOAT, " +
                    "Date TEXT, " +
                    "AccountIndex INTEGER, " +
                    "FOREIGN KEY(OperationCategoryIndex) REFERENCES OperationCategoryTable(Index), " +
                    "FOREIGN KEY(AccountIndex) REFERENCES AccountTable(Index)";
                command.ExecuteNonQuery();
            }

            InitializeOperationCategories();

            InitializeCurrencies();
        }

        void InitializeOperationCategories()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                SQLiteCommand command = new SQLiteCommand(connection);
                command.CommandText = $"INSERT INTO OperationCategoryTable(Name, OperationTypeIndex) VALUES" +
                    $"({OperationCategoryEnum.Accomodation.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Cafe.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Car.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Clothes.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Communication.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Entertainments.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Food.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Health.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Hygiene.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Pets.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Presents.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Sports.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Taxi.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Transport.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString())}), " +
                    $"({OperationCategoryEnum.Deposits.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString())}), " +
                    $"({OperationCategoryEnum.Salary.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString())}), " +
                    $"({OperationCategoryEnum.Saving.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString())}), " +
                    $"({OperationCategoryEnum.Transaction.ToString()}, {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString())})";
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
                    CurrencyTable.Add(currency);
                }
            }

            if (Currencies.FirstOrDefault(c => c.Code == "AZN") == null)
            {
                Currency aznCurrency = new Currency
                {
                    Code = "AZN",
                    Value = 1
                };

                CurrencyTable.Add(aznCurrency);
            }
        }

        public void Load()
        {
            AccountTable.Load();
            CurrencyTable.Load();
            OperationTable.Load();
            OperationTypeTable.Load();
            OperationCategoryTable.Load();
        }

        public void Save()
        {
            SaveChanges();
        }
    }
}
