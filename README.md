# Oblivion

Oblivion is a system designed for the management of a chain of gaming centers. It was developed as part of Project 1 during the third year at the Faculty of Electrical Engineering in East Sarajevo. This application provides efficient tools for managing gaming centers, allowing administrators of gaming centers to oversee available computers, and enabling employees to manage games and services within the gaming centers. The application is developed as a desktop application using WPF .NET, with data stored in a MySQL database. Models and prototypes of the application are stored in the repository.

## System Functionalities

### 1. System Access
- **Login:** Users can log in to the system as administrators or employees. Administrators have the authority to manage internal elements of the system, such as adding employees, computers, and services. Employees can manage external aspects of the system, including game installations and billing for gaming center services.
- **Logout:** Users can log out of the system, but employees cannot log out until they have settled all outstanding bills.
- **Employee Data Overview:** Administrators can access data for all employees in the gaming center.

### 2. Employee Management
- **Granting System Access to Employees:** Administrators can create accounts for new employees by assigning them unique usernames and passwords.
- **Editing Employee Information:** Administrators can modify employee salaries, residence details, and grant administrative privileges.
- **Employee Deletion:** Administrators can delete employee accounts that are no longer in use, with all associated records stored in a separate file.
- **Change Username and Password:** Users are allowed to change the username and password assigned by the administrator.

### 3. Computer Management
- **Computer Overview:** Administrators can view all computers within the gaming center, including their individual components.
- **Adding Computers:** Administrators can add new computers along with their specifications. Each computer is added as a separate entity, and its components are filled in individually.
- **Component Management:** Administrators can modify data for existing computers and their specifications.
- **Computer Removal:** Administrators can remove any computer that is no longer functional.

### 4. Service Management
- **Service Overview:** Administrators can review all services provided within the gaming center and make changes to service prices.
- **Billing Overview:** Administrators can access records of all bills that have been paid.

### 5. Computer Usage
- **Computer Monitoring:** Users can monitor events on the computers, including remaining time, player arrival time, and payment time.
- **Reserving Computers:** Users can reserve a computer by providing their name, selecting games, preferred time, and additional services.
- **Billing:** Users can settle their bills after the reserved time expires, and a bill is issued.
- **Adding Services to Players:** Users can extend their gaming time and add extra services to their accounts.
- **Game Search:** The system allows users to search for computers by game name, displaying information about the game type and the computer where it's available.
- **Bill Storage:** The system maintains records of all issued bills in the database.

## Technical Specifications

- The application is developed as a desktop application using WPF .NET (.NET Framework 4.5).
- Data is stored in a MySQL database.

## Prototype Startup

To run the prototype, use the following specifications:

- .NET Framework 4.5
- Visual Studio Version: 16.0.31205.134
- Minimum Visual Studio Version: 10.0.40219.1

The MySQL database is required for the application, and the script can be found at the following path in the repository:

`Oblivion-Baza/igraonica.sql`

## Authors

This project was developed by students of the Faculty of Electrical Engineering in East Sarajevo as part of Project 1 during the third year. The authors are:

- Danijela Milanovic (https://github.com/DanijelaMilanovic)
- Stefan Jokic

## License

This application was created for educational purposes and is released under an open license. Feel free to use and adapt it to your needs.

## Contact

For any additional information or questions, you can contact the authors at danijelamilanovic222@gmail.com, stefan.jokic99@hotmail.com
