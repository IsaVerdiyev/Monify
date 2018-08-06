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

        public ObservableCollection<Account> Accounts { get => accounts.Local; set { } }
        public ObservableCollection<OperationType> OperationTypes { get => operationTypes.Local; set { } }
        public ObservableCollection<OperationCategory> OperationCategories { get => operationCategories.Local; set { } }
        public ObservableCollection<Operation> Operations { get => operations.Local; set { } }
        public ObservableCollection<Currency> Currencies { get => currencies.Local; set { } }

        public DateTime CurrenciesDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTime LastUpdateDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

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
                Load();
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
