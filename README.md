# Grat Shfit Save API

#### By Eliot Gronstal 5.11.23

#### A web API the allows users to register and login and save their gratuity-related income in a databse to track over time.

## Technologies Used

* _C#/.NET 6.0_
* _MySQL Workbench_
* _Entity Framework_
* _Identity Framework_
* _JWT_
* _Swagger_
* _Postman_

## Description



There are custom endpoints for some of these user stories.

* A user can GET and POST reviews about a park.

* A user can GET reviews about a park.

* A user can PUT and DELETE reviews.

* A user can access the API endpoint with a query parameter that specifies the page that should be returned.

* A user can query the `random` API endpoint which will randomly select a park for a user.

## Setup/Installation Requirements
_Requires console application such as Git Bash, Terminal, or PowerShell_

1. Open Git Bash or PowerShell if on Windows and Terminal if on Mac
2. Run the command

    ``git clone https://github.com/elgrons/GratShiftSaveApi.Solution``

3. Run the command

    ``cd ParksDirectoryApi.Solution``

* You should now have a folder `GratShiftSaveApi` with the following structure.
    <pre>GratShiftSaveApi.Solution
    ├── .gitignore 
    ├── ... 
    └── GratShiftSaveApi
        ├── Controllers
        ├── Models
        ├── ...
        ├── README.md</pre>

4. Add a file named appsettings.json in the following location, inside the GratShiftSaveApi folder 

    <pre>GratShiftSaveApi.Solution
    ├── .gitignore 
    ├── ... 
    └── GratShiftSaveApi
        ├── Controllers
        ├── Models
        ├── ...
        └── <strong>appsettings.json</strong></pre>
      
5. Copy and paste below JSON text in appsettings.json.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=3306;database=parks_api;uid=[YOUR-USERNAME-HERE];pwd=[YOUR-PASSWORD-HERE];"
  }
}

```

7. Replace [YOUR-USERNAME-HERE] with your MySQL user name.

8. Replace [YOUR-PASSWORD-HERE] with your MySQL password.

9. Run the command

    ```dotnet ef database update```


<strong>To Run</strong>

Navigate to the following directory in the console
    <pre>GratShiftSaveApi.Solution
    └── <strong>GratShiftSaveApi</strong></pre>

Run the following command in the console

  ``dotnet build``

Then run the following command in the console

  ``dotnet run``

This program was built using _`Microsoft .NET SDK 6.0`_, and may not be compatible with other versions. Cross-version performance is neither implied nor guaranteed, your actual mileage may vary.

## API Documentation
Explore the API endpoints in Postman or a browser. You will not be able to utilize authentication in a browser.

###  Swagger Documentation 
To view the Swagger documentation for the ParksApi, launch the project using `dotnet run` using Terminal or Powershell, then input the following URL into your browser: `https://localhost:5001/swagger/index.html`

![swaggerendpoints](SwaggerEndpoints.png)

### Parks

Get information about different national and state parks.

#### HTTP Request Structure
```
GET https://localhost:5001/api/GratShift/
GET https://localhost:5001/api/GratShift/{id}
POST https://localhost:5001/api/GratShift/
PUT https://localhost:5001/api/GratShift/{id}
DELETE https://localhost:5001/api/GratShift/{id}
GET https://localhost:5001/api/GratShift/page/{page}
GET https://localhost:5001/api/GratShift/random
```
* To utilize the POST request and create a new instance of a destination, the following information is required.
```
{
    "parkId": "int",
    "name": "string",
    "location": "string",
    "review": "string",
    "rating": "int"
}
```

#### Example Query
```
https://localhost:5001/api/GratShift/1
```
#### Sample JSON Response
```
{   
    "parkId": 1,
    "name": "Crater Lake National Park",
    "location": "Oregon",
    "review": "Excellent",
    "rating": 10
}
```
## Pagination

* Paging refers to getting a smaller selection of results from the ParksApi and browsing through them page by page.
* Example pagination endpoint: Change the page number in the URL: https://localhost:5001/api/Parks/page/1

## Corresponding React App

<!-- A work-in-progress corresponding React app can be found at [https://github.com/elgrons/ParksDirectoryClient.Solution](https://github.com/elgrons/ParksDirectoryClient.Solution) -->

## Known Bugs

* _No known issues_

* _Reach out with any questions or concerns to [eliot.lauren@gmail.com](eliot.lauren@gmail.com)_

## License

[MIT](/LICENSE)

Copyright 2023 Eliot Gronstal