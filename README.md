# Instant Delivery
Instant Delivery is a sample courier company management system written in .NET technologies (and some AngularJS as well). We developed it for an Enterprise Applications in .NET course at our university.

The system consists of three main modules:
* RESTful API service providing common interface for client applications
* WPF client application for the company's employees
* web application for the company's clients (ASP.NET MVC with an AngularJS SPA module)

## Desktop application
The desktop app is dedicated to the company's employees who may belong to one of the three following groups:
* administrative employees - who can manage other employees, their vehicles, add new packages and assign them to couriers
* couriers - who have access to data of the packages they need to deliver
* administrators - who can manage user accounts

## Web application
The web app is dedicated to the company's clients and consists of the main website providing general information and package tracking capabilities and a SPA module for logged in users. The main features of the portal include:
* sending packages
* viewing and showing statistics of sent packages

## Installation
You can easily run Instant Delivery locally using the LocalDb database (provided by Visual Studio) or a SQL Server instance (in that case you should update the connection string in `Web.config` for InstantDelivery.Service project). To initialize the database and seed it with the test data run the following command in the Package Manager Console:
```
Update-Database -ProjectName InstantDelivery.Domain -StartUpProject InstantDelivery.Service
```
You might also want to update the API service URL in `InstantDelivery.Web/App/app.js` and `InstantDelivery.Presentation/App.config`.

#### Sending email notifications
The system is configured to send email notifications to its users. For it to work, you'll need to add a file named `AppSettingsSecrets.config` to the root folder of the InstantDelivery.Service project and include your Gmail credentials inside:
```xml
<appSettings>
  <add key="NotificationEmailUsername" value="gmail_username" />
  <add key="NotificationEmailPassword" value="password" />
</appSettings>
```
#### User accounts
The test database contains some test user accounts: employee:employee123, courier:courier123, admin:admin123, customer:customer123.

## License
MIT License
