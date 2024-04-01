# WeatherApp
## About project
The project is a web application that allows users to search current weather conditions for selected cities around the world. The searched weather is automatically added to the database. There is an option to save a specific weather in the search history and to delete the weather later. The application fetches weather data from an external API using Json.NET: https://openweathermap.org. Additionally, unit tests have been conducted using xUnit tool.

## Technologies
- b a c k e n d:
  - **ASP.NET Core Web API**
    - REST API
  - **C#**
    - Json.NET
    - Asynchronous programming (async/await)
    - LINQ Queries
    - Dependency Injection, Repository, DTOs patterns
    - Configuration management with appsettings.json (to read Api Key and database connection)
    - ClosedXML
  - ORM: **Entity Framework Core**
    - Migrations
  - Database: **Microsoft SQL Server**
    - One-to-one relationship
  - Unit tests: **xUnit**
  - API testing by:
    - **Swagger UI**
    - **Postman**
- f r o n t e n d:
  - **React**
    - JavaScript
    - HTML
    - CSS
    - React Bootstrap components
    - React Router
    - Hooks
    - Asynchronous programming (async/await)

## Images
Home page:
![Home page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/1.png)

Searched weather for Los Angeles:
![Searched weather](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/2.png)

Sun info page:
![Sun info page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/3.png)

Searched sun information for Los Angeles:
![Searched sun information](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/4.png)

History page:
![History page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/5.png)

Weather details for Los Angeles:
![Weather details](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/6.png)

History page after searching by city name:
![History page2](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/7.png)

Printed history data in Excel file using ClosedXML:
![Printed history data](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/8.png)

History page after clicking Save button:
![History page3](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/9.png)

Saved page:
![Saved page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/10.png)

Printed saved weather data in Excel file using ClosedXML:
![Printing saved weather data](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/11.png)

Mobile version: <br/>
![Mobile version](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/11.2.png)

Unit testing using xUnit:
![Unit testing](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/12.png)
