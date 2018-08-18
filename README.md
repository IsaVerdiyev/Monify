#Monify
Monify is a window application for accounting for own costs and revenues.  
The application is written in C# with the use of 'WPF'.   
Application's data is saved in database by using 'SQLite' (though saving in file is also implemented in code which you can use by changing type of created IStorage in 'StorageGetter' file).  

The application is based on the popular mobile application Monify.  

Application can:   
	-Allow you to add new accounts with some initial balance on them and of chosen currency.  
	-Allow you to make transactions from one account to another (application takes into account different currencies during the transaction and converts them before making the transaction).  
	-Allow you to add your revenues and costs(expenses) and mention their categories.  
	-Show balance of the chosen account and also can show total balance from all accounts (balance is counted by converting balance from each account to chosen main currency for all accounts).  
	-Show your expenses and revenues based on chosen by you time interval, date and account.  
	-Change language (words are translated by using Yandex translater api).  
	-Update currencies by downloading currency data from bank api.  
	
'Application was written for testing own abilities in programming only'.
