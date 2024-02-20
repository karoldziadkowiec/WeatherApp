# WeatherApp (in development)

The project is a web application for checking the current weather. It enables users of the website to search current weather conditions for a selected city in the world. The searched weather is automatically added to the database. There is an option to save a specific weather in the search history and to delete the weather later. The application downloads weather data from an external API: https://openweathermap.org.

Technologies used in the project:
- b a c k e n d:
  - Framework: **ASP.NET Core Web API**
    - REST API
  - **C#**
    - Json.NET
    - Multithreading (async/await)
    - LINQ Queries
    - Repository and DTOs pattern
    - Configuration Management with AppSettings (to read Api Key)
    - ClosedXML
  - ORM: **Entity Framework Core**
    - Migrations
  - Database: **Microsoft SQL Server**
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
    - Multithreading (async/await)

Home page:

![Home page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/1.png)

Searched weather:

![Searched weather](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/2.png)

Sun info page:

![Sun info page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/3.png)

Searched sun information:

![Searched sun information](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/4.png)

History page:

![History page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/5.png)

Weather details:

![Weather details](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/6.png)

History page after searching by city name:

![History page2](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/7.png)

Printing history data to Excel file by ClosedXML:

![Printed history data](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/8.png)

History page after clicking Save button:

![History page3](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/9.png)

Saved page:

![Saved page](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/10.png)

Printing saved weather data to Excel file by ClosedXML:

![Printing saved weather data](https://github.com/karoldziadkowiec/WeatherApp/blob/master/github-images/11.png)
