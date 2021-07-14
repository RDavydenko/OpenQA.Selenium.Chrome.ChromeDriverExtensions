# OpenQA.Selenium.Chrome.ChromeDriverExtensions
This extensions allows you to add an HTTP-proxy with authentication (username and password) to Selenium.ChromeDriver

## Install NuGet package
```
Install-Package OpenQA.Selenium.Chrome.ChromeDriverExtensions -Version 1.0.0
```

## Using
``` csharp
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chrome.ChromeDriverExtensions;
...
var options = new ChromeOptions();

// Add your HTTP-Proxy
options.AddHttpProxy(proxyHost, proxyPort, proxyUserName, proxyPassword);

var driver = new ChromeDriver(options); // or new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);

driver.Navigate().GoToUrl("https://whatismyipaddress.com/"); // Check your IP
```
