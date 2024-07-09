Project Title : Expense Tracker App
Type :   .NET Desktop Application
To Check UI Of Expense Tracker Open About Project File:

1. Abstract: In today's financial-conscious era, effective expense management is crucial. Our proposed project is to develop a desktop expense tracker application using .NET framework, C#, and SQL for database management. This application will enable users to monitor spending, manage budgets, analyze expenses, and make informed financial decisions.
2. Background and Introduction: The Desktop Expense Tracker App aims to address the challenges individuals face in managing personal finances. It provides a user-friendly tool for tracking daily expenses, setting budgets, and analyzing financial data. The project aligns with the needs of users seeking a reliable solution for personal expense management.
3. Project Rationale: The project's purpose is to enhance financial literacy and discipline among users by offering a practical tool for managing finances. By developing this application, we aim to improve financial stability and informed decision-making.
4. Aims and Objectives:
⦁	Develop a user-friendly desktop application for expense tracking using .NET framework and C#.
⦁	Enable users to input salary and set budgets for various expense categories.
⦁	Allow users to add, update, and delete daily expenses with details such as date, amount, category, and notes.
⦁	Implement features to restrict spending based on predefined budget limits.
⦁	Provide users with the ability to view remaining salary, budget, and track financial statistics monthly and yearly.
5. Proposed Methodology:
⦁	Planning and Design: Define requirements, design user interface, and plan database schema.
⦁	Frontend and Backend Development: Develop frontend using .NET WinForms or WPF, implement backend business logic using C#, and establish SQL Server database.
⦁	Database Design and Implementation: Design and implement database schema using SQL Server.
⦁	Testing and Quality Assurance: Conduct unit testing, integration testing, and user acceptance testing.
⦁	Deployment and User Feedback: Deploy the application, collect user feedback, and make final adjustments.
6. Proposed Solution and Anticipated Results: The system will provide:
⦁	Secure login system using .NET Identity for user authentication.
⦁	Salary and budget management, expense tracking, budget restrictions, and expense management features.
⦁	Financial analysis through reports and visualization of monthly and yearly expenses, budget utilization, and savings trends.
7. References:
⦁	Mint, YNAB, PocketGuard, Expense IQ, GoodBudget, Wally, Spendee, Personal Capital, Expensify, Zoho Expense.


Whole explanation of modules that are used in the whole project :
Modules of Class User
In the provided C# code, the User class is implemented to perform basic CRUD (Create, Read, Update, Delete) operations on a user entity in a database. Here are the modules/functions defined within the class:
⦁	Properties:
⦁	UserId: Integer, stores the user's ID.
⦁	Email: String, stores the user's email address.
⦁	Password: String, stores the user's password.
⦁	DateOfBirth: String, stores the user's date of birth.
⦁	connectionString: String, the connection string used to connect to the SQL Server database.
⦁	Constructor:
⦁	No explicit constructor is defined, so the default constructor is used.
⦁	Methods:
⦁	Register(): Registers a new user by inserting their details (Email, Password, DateOfBirth) into the user_table in the database.
⦁	Update(): Updates an existing user's details (Email, Password, DateOfBirth) in the user_table based on UserId.
⦁	Delete(): Deletes a user from the user_table based on UserId.
⦁	Login(): Authenticates a user by checking if the provided Email and Password match any record in the user_table. If a match is found, it sets the UserId and DateOfBirth properties.
⦁	Exception Handling:
⦁	Various try-catch blocks are used to handle exceptions that may occur during database operations (SqlException) or other errors (Exception). Error messages are printed to the console.
⦁	SQL Queries:
⦁	SQL queries (INSERT, UPDATE, DELETE, SELECT) are used to interact with the user_table.
⦁	Based on the above description, the distinct modules or functionalities encapsulated within the User class are:
⦁	Registration Module:
⦁	Register() method handles user registration by inserting user details into the database.
⦁	Update Module:
⦁	Update() method handles updating user details in the database.
⦁	Deletion Module:
⦁	Delete() method handles deleting a user from the database.
⦁	Login Module:
⦁	Login() method handles user authentication and retrieves user information upon successful login.
Additional Modules (Not Explicitly Defined):
⦁	Retrieving User Profile:
⦁	Although not explicitly mentioned, the Login() method indirectly retrieves the user profile upon successful authentication.
Conclusion:
The User class effectively implements the necessary modules (Register, Update, Delete, Login) for basic user management functionalities (CRUD operations) in an expense tracking application. These modules handle user interactions with a SQL Server database, providing basic error handling and feedback through console messages.
Modules of Class Salary
In the provided C# code for the Salary class, the class is designed to handle operations related to managing salary records for users in an expense tracking application. Here are the modules or functionalities defined within this class:
⦁	Properties:
⦁	SalaryId: Integer, stores the salary record ID.
⦁	UserId: Integer, stores the user ID associated with the salary record.
⦁	SalaryAmount: Double, stores the amount of salary.
⦁	Date: String, stores the date associated with the salary record.
⦁	Note: String, stores any notes related to the salary record.
⦁	connectionString: String, the connection string used to connect to the SQL Server database.
⦁	Methods:
⦁	Add(): Adds a new salary record for a user. If a salary record already exists for the user, it updates the existing record by adding to the current salary amount.
⦁	Update(): Updates an existing salary record in the salary_table based on SalaryId.
⦁	Delete(): Deletes a salary record from the salary_table based on SalaryId.
⦁	Retrieve(): Retrieves the salary amount for a user (UserId) from the salary_table.
⦁	Exception Handling:
⦁	try-catch blocks are used to handle exceptions that may occur during database operations (SqlException) or other errors (Exception). Error messages are printed to the console.
⦁	SQL Queries:
⦁	SQL queries (SELECT, INSERT, UPDATE, DELETE) are used to interact with the salary_table in the SQL Server database.
Modules in the Salary Class
Based on the above description, the distinct modules or functionalities encapsulated within the Salary class are:
⦁	Insert/Add Module:
⦁	Add() method handles adding new salary records for users. If a record already exists, it updates the existing record by adding to the current salary amount.
⦁	Update Module:
⦁	Update() method updates existing salary records in the database.
⦁	Delete Module:
⦁	Delete() method deletes salary records from the database.
⦁	Retrieve Module:
⦁	Retrieve() method retrieves the salary amount for a specific user from the database.
Additional Modules (Not Explicitly Defined):
⦁	Retrieve User Salary:
⦁	The Retrieve() method directly retrieves the salary amount for a user, fulfilling the requirement to retrieve user salary.
Conclusion:
The Salary class effectively implements the necessary modules (Add, Update, Delete, Retrieve) for managing salary records in an expense tracking application. These modules handle user interactions with a SQL Server database, providing basic error handling and feedback through console messages.
Modules of Budget class
In the Budget class provided, which aims to manage budget records for users in an expense tracking application, several modules or functionalities can be identified:
⦁	Properties:
⦁	BudgetId: Integer, stores the budget record ID.
⦁	UserId: Integer, stores the user ID associated with the budget record.
⦁	Amount: Double, stores the amount of the budget.
⦁	Type: String, specifies the type of budget.
⦁	Date: String, stores the date associated with the budget record.
⦁	Note: String, stores any notes related to the budget record.
⦁	connectionString: String, the connection string used to connect to the SQL Server database.
⦁	Methods:
⦁	Add(): Adds a new budget record for a user. If a budget record with the same type exists, it updates the existing record by adding to the current budget amount.
⦁	Update(int bId): Updates an existing budget record in the budget_table based on BudgetId.
⦁	Delete(int bId): Deletes a budget record from the budget_table based on BudgetId.
⦁	Retrieve(int UserId): Retrieves a list of budget records for a specific user from the budget_table.
⦁	DeductBudgetFromSalary(Budget budget): Helper method that deducts the budget amount from the user's salary and updates the salary_table accordingly.
⦁	Exception Handling:
⦁	try-catch blocks are used to handle exceptions that may occur during database operations (SqlException) or other errors (Exception). Error messages are typically outputted to the console.
⦁	SQL Queries:
⦁	SQL queries (SELECT, INSERT, UPDATE, DELETE) are used to interact with the budget_table in the SQL Server database.
Modules in the Budget Class
Based on the provided code, the distinct modules or functionalities encapsulated within the Budget class are:
⦁	Insert/Add Module:
⦁	Add() method handles adding new budget records for users. If a record already exists for the same type, it updates the existing record by adding to the current budget amount.
⦁	Update Module:
⦁	Update(int bId) method updates existing budget records in the database based on BudgetId.
⦁	Delete Module:
⦁	Delete(int bId) method deletes budget records from the database based on BudgetId.
⦁	Retrieve Module:
⦁	Retrieve(int UserId) method retrieves a list of budget records for a specific user from the database.
⦁	Deduct Budget from Salary Module:
⦁	DeductBudgetFromSalary(Budget budget) method deducts the budget amount from the user's salary and updates the salary_table accordingly.
Additional Modules (Not Explicitly Defined):
⦁	Retrieve User Budgets:
⦁	The Retrieve(int UserId) method directly retrieves budget records for a specific user, fulfilling the requirement to retrieve user budgets.
Conclusion:
The Budget class effectively implements the necessary modules (Add, Update, Delete, Retrieve, Deduct Budget from Salary) for managing budget records in an expense tracking application. These modules handle user interactions with a SQL Server database, provide basic error handling, and ensure the necessary updates and operations are performed when managing user budgets.
Modules of Expense class
In the Expense class provided, which aims to manage expense records for users in an expense tracking application, several modules or functionalities can be identified:
⦁	Properties:
⦁	EId: Integer, primary key auto-generated for expense record.
⦁	UId: Integer, foreign key referencing user table.
⦁	BId: Integer, foreign key referencing budget table.
⦁	Date: String, expense date.
⦁	Time: String, expense time.
⦁	Category: String, expense category.
⦁	Amount: Double, expense amount.
⦁	Note: String, additional note related to the expense.
⦁	Type: String, type associated with the budget (from the budget_table).
⦁	Methods:
⦁	Add(): Adds a new expense record for a user and deducts the amount from the associated budget.
⦁	Update(int eId): Updates an existing expense record in the expense_table based on EId.
⦁	DeleteAndUpdateBudget(int eId, double amount): Deletes an expense record and updates the associated budget with the refunded amount.
⦁	RetrieveExpensesWithType(int uId): Retrieves expenses with their associated budget type for a specific user.
⦁	Exception Handling:
⦁	try-catch blocks are used to handle exceptions that may occur during database operations (SqlException) or other errors (Exception). Error messages are typically outputted to the console.
⦁	SQL Queries:
⦁	SQL queries (SELECT, INSERT, UPDATE, DELETE) are used to interact with the expense_table, budget_table, and manage transactions (SqlTransaction) when necessary.
Modules in the Expense Class:
Based on the provided code, the distinct modules or functionalities encapsulated within the Expense class are:
⦁	Insert/Add Module:
⦁	Add() method handles adding new expense records for users and deducts the amount from the associated budget.
⦁	Update Module:
⦁	Update(int eId) method updates existing expense records in the database based on EId.
⦁	Delete Module:
⦁	DeleteAndUpdateBudget(int eId, double amount) method deletes expense records from the database based on EId and updates the associated budget with the refunded amount.
⦁	Retrieve Module:
⦁	RetrieveExpensesWithType(int uId) method retrieves expenses with their associated budget type for a specific user from the database.
Additional Modules (Not Explicitly Defined):
⦁	Retrieve User Expenses:
⦁	The RetrieveExpensesWithType(int uId) method directly retrieves expense records for a specific user, fulfilling the requirement to retrieve user expenses.
Conclusion:
The Expense class effectively implements the necessary modules (Add, Update, DeleteAndUpdateBudget, RetrieveExpensesWithType) for managing expense records in an expense tracking application. These modules handle user interactions with a SQL Server database, provide basic error handling, and ensure the necessary updates and operations are performed when managing user expenses.
Modules of Category class 
In the Category class provided, which aims to manage categories for individual users in an expense tracking application, several modules or functionalities can be identified:
⦁	Properties:
⦁	CId: Integer, primary key for the category.
⦁	UserId: Integer, foreign key referencing user table.
⦁	Cate: String, category name.
⦁	Methods:
⦁	Add(): Adds a new category for a specific user.
⦁	Update(int cId): Updates an existing category based on CId.
⦁	Delete(int cId): Deletes a category based on CId.
⦁	Retrieve(int userId): Retrieves categories associated with a specific user.
⦁	Exception Handling:
⦁	try-catch blocks are used to handle exceptions that may occur during database operations (SqlException) or other errors (Exception). Error messages are typically outputted to the console.
⦁	SQL Queries:
⦁	SQL queries (SELECT, INSERT, UPDATE, DELETE) are used to interact with the category_table in the SQL Server database.
Modules in the Category Class
Based on the provided code, the distinct modules or functionalities encapsulated within the Category class are:
⦁	Insert/Add Module:
⦁	Add() method handles adding new category records for users.
⦁	Update Module:
⦁	Update(int cId) method updates existing category records in the database based on CId.
⦁	Delete Module:
⦁	Delete(int cId) method deletes category records from the database based on CId.
⦁	Retrieve Module:
⦁	Retrieve(int userId) method retrieves categories associated with a specific user from the database.
Conclusion:
The Category class effectively implements the necessary modules (Add, Update, Delete, Retrieve) for managing category records in an expense tracking application. These modules handle user interactions with a SQL Server database, provide basic error handling, and ensure the necessary updates and operations are performed when managing user categories.
By combinig the modules of that expense tracker app lets draw the Class Diagram



Summary 
The Desktop Expense Tracker App is a comprehensive solution designed to address the challenges individuals face in managing personal finances. Leveraging the robustness of the .NET framework, C#, and SQL Server, the application provides a user-friendly interface for users to track expenses, manage budgets, and analyze financial data. Key features include salary and budget management, daily expense tracking, budget restrictions, and detailed financial analysis through reports and visualizations.
The project methodology involved meticulous planning, detailed design, and iterative development to ensure a seamless user experience. The application utilizes .NET WinForms/WPF for the frontend, C# for backend business logic, and SQL Server for efficient database management. Extensive testing and user feedback were integral to refining the application, ensuring it meets user expectations and provides reliable financial management capabilities.
By implementing this Desktop Expense Tracker App, we aim to enhance users' financial literacy and discipline, contributing to improved financial stability and informed decision-making. The application not only empowers users to manage their finances effectively but also provides valuable insights into spending habits and budget utilization.
In conclusion, the Desktop Expense Tracker App is poised to become a vital tool for anyone seeking to take control of their financial health. It combines user-friendly design with powerful functionality to deliver a robust solution for personal expense management.


 

 

 
 
 




(Ended)
