# AspNetCore Curl Middleware Example
Project demostrate the capturing of ASP.NETCore HTTP Request and converting it as CURL files.

# Use Case 
As a back-end developer we would come across with different errors during runtime across multiple environments like QA, Staging, Production etc...In the modern era of micro-services the effort that we put to reproduce the same request and test in our local system has become a tedious process. In order to reduce the effort spent in reproducing a issue we could log the HTTP Request object as a curl request and save the request as files. These files can be pulled back into Postman or any other API testing tools and can be used to reproduce the issue a lot quicker than manually framing it.

# Usage
Install the AspNetCoreCurlMiddleware package using the following command :

```Install-Package AspNetCoreCurlMiddleware -Version 1.0.0``` .

Add the extensions methods from the package in **ConfigureServices**  and **Configure** methods of **Startup.cs** are :

```
services.AddSaveAsCurlMiddlewareServices(new Middleware.Models.SaveRequestOptions { LogPath = "request", SaveRequest = true });
```

```
app.UseSaveAsCurlMiddleware();
```

The SaveRequestOptions carries the input parameters for the curl builder you can customize the curl builder by extending the classes for your needs.


<a href="https://www.buymeacoffee.com/hakunamatatain" target="_blank"><img src="https://cdn.buymeacoffee.com/buttons/default-orange.png" alt="Buy Me A Coffee" height="41" width="174"></a>
