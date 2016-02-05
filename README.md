# Instant Delivery
Instant Delivery is a sample courier company management system written in .NET technologies (and some AngularJS as well). We developed it for an Enterprise Applications in .NET course at our university.

The system consists of three main modules:
* RESTful API service providing common interface for client applications
* WPF client application for the company's employees
* web application for the company's clients (ASP.NET MVC with an AngularJS SPA module)

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
## License
MIT License
