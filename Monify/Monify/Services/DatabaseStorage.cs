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
            SQLiteConnection connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            try { 
            connection.Open();
            
                string sqlString = "CREATE TABLE CurrencyTable (" +
                    "Id INTEGER PRIMARY KEY, " +
                    "Code NVARCHAR(10), " +
                    "Value FLOAT)";
                SQLiteCommand command = new SQLiteCommand(sqlString, connection);
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE OperationTypeTable (" +
                    "Id INTEGER PRIMARY KEY, " +
                    "Name NVARCHAR(30))"; 
                command.ExecuteNonQuery();

                command.CommandText = $"INSERT INTO OperationTypeTable(Name) VALUES" +
                    $"('{OperationTypesEnum.Expense.ToString()}'), " +
                    $"('{OperationTypesEnum.Profit.ToString()}')";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE OperationCategoryTable (" +
                    "Id INTEGER  PRIMARY KEY, " +
                    "Name NVARCHAR(30), " +
                    "OperationTypeIndex INTEGER, " +
                    "FOREIGN KEY(OperationTypeIndex) REFERENCES OperationTypeTable(Id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE AccountTable(" +
                    "Id INTEGER PRIMARY KEY, " +
                    "Name NVARCHAR(30), " +
                    "CurrencyIndex INTEGER, " +
                    "StartDate DATETIME, " +
                    "FOREIGN KEY(CurrencyIndex) REFERENCES CurrencyTable(Id))";
                command.ExecuteNonQuery();

                command.CommandText = "CREATE TABLE OperationTable(" +
                    "Id INTEGER PRIMARY KEY, " +
                    "OperationCategoryIndex INTEGER, " +
                    "Amount FLOAT, " +
                    "Date DATETIME, " +
                    "AccountIndex INTEGER, " +
                    "FOREIGN KEY(OperationCategoryIndex) REFERENCES OperationCategoryTable(Id), " +
                    "FOREIGN KEY(AccountIndex) REFERENCES AccountTable(Id))";
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
                command.CommandText = $"INSERT INTO OperationCategoryTable(Name, OperationTypeIndex) VALUES('{OperationCategoryEnum.Accomodation.ToString()}', {OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id})";
                //$"('{OperationCategoryEnum.Accomodation.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Cafe.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Car.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Clothes.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Communication.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Entertainments.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Food.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Health.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Hygiene.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Pets.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Presents.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Sports.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Taxi.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Transport.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Expense.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Deposits.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Salary.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Saving.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}'), " +
                //$"('{OperationCategoryEnum.Transaction.ToString()}', '{OperationTypes.FirstOrDefault(t => t.Name == OperationTypesEnum.Profit.ToString()).Id}')";

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
