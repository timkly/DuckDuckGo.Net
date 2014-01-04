# DuckDuckGo.Net 

A DotNet instant answer library written in C# for the DuckDuckGo Instant Answer API

> Access the [DuckDuckGo](https://duckduckgo.com/api) API with [C#].

> Version 1.0.0


## Requirements
> Target framework - 4

> System.Web to be referenced if using client profile option


## Installation
To install, download project and reference the DuckDuckGo.Net library in your project

Via [GitHub](https://github.com/timkly/DuckDuckGo.Net)

Via [NuGet] (https://www.nuget.org/packages/DuckDuckGo.Net/)

Nuget Console: 
```csharp
PM> Install-Package DuckDuckGo.Net
```

## Usage
Once you have referenced the library in your project, ensure you include a using reference in the file which will be consuming it
```csharp
using DuckDuckGo.Net;
```

Create a new instance of the DuckDuckGo Search using the default settings
```csharp
var search = new Search();
```

Create a new instance of the DuckDuckGo Search using custom settings during initialisation (all settings shown).
```csharp
var search = new Search
{
    NoHtml = true,
    NoRedirects = true,
    IsSecure = true,
    SkipDisambiguation = true,
    ApiClient = new HttpWebApi()
};
```

Settings may also be configured after initialisation but before the query is performed
```csharp
search.NoHtml = false;
search.ApiClient = new FileApi();
```

Perform a query and return the result as a SearchResult object
```csharp
var searchResult = search.Query("apple", ApplicationName);
```

Perform a query and return the result as a JSON formatted string 
```csharp
var jsonString = search.TextQuery("apple", ApplicationName, ResponseFormat.Json);
```

Perform a query and return the result as a XML formatted string
```csharp
var xmlString = search.TextQuery("apple", ApplicationName, ResponseFormat.Xml);
```


# Contact
Github - [timkly](http://github.com/timkly)

Twitter - [@timkly](http://twitter.com/timkly)

# LICENSE
MIT LICENSE--see file /LICENSE 

**This project has no affiliation with the company DuckDuckGo.**      
  
          