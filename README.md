# DuckDuckGo.Net 

A dot.net instant answer library written in C# for the DuckDuckGo Search API

> Access the [DuckDuckGo](https://duckduckgo.com/api) API with [C#].

> Version 0.0.1


## Installation

To install, download project and reference the DuckDuckGo.Net library in your project

## Usage
Once you have referenced the library in your project, ensure you include a using reference in the file which will be consuming it
```csharp
using DuckDuckGo.Net;
```

Return ResultSet object using WebClient loader (used by default)
```csharp
var results = Search.Query("Search Query", "Your User Agent");
```

Return ResultSet object from file query (useful for testing non API related integration)
```csharp
var results = Search.Query("Search Query", "Your User Agent", Loader.File);
```

Return ResultSet object using WebClient loader with Redirects switched off
```csharp
var results = Search.Query("Search Query", "Your User Agent", null, true);
```

Return ResultSet object using WebClient loader with No Html included
```csharp
var results = Search.Query("Search Query", "Your User Agent", null, false, true);
```

Return ResultSet object using WebClient loader with Disambiguation ignored
```csharp
var results = Search.Query("Search Query", "Your User Agent", null, false, false, true);
```  

Return query as JSON string
```csharp
var results = Search.Fetch("Search Query", "Your User Agent");
```

Return query as XML string
```csharp
var results = Search.Fetch("Search Query", "Your User Agent", Format.Xml);
```


# Contact
Github - [timkly](http://github.com/timkly)

Twitter - [@timkly](http://twitter.com/timkly)

# TODO
Add test cases 

Improve documentation

Add new loaders

# LICENSE
MIT LICENSE--see file /LICENSE 

**This project has no affiliation with the company DuckDuckGo.**      
  
          