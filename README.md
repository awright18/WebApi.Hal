# WebApi.Hal
A Web API Server configuration based hal+json media type formatter

## Create Hal type configurations 

1. Create a class that implements HalTypeConfiguration<T>

2. Override the AddLinks<T>(T value) method so you can add the appropriate links for your type.

Example: 

```
public class AuthorHalConfiguration : HalTypeConfiguration<Author>
{
    public AuthorHalConfiguration() : base("/api/author")
    {
    }

    protected override void AddLinks(Author value)
    {
        if (value == null)
            return;

        AddLink("self", value.Id.ToString()); //add self link
        AddLinkTemplate("find", "{?id}"); //add find link
    }
}

```

## Create A HalConfig file

1. In the App_Start directory of your web api project create a HalConfig.cs file
2. Add the following code to this file
```
 public static class HalConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.AddHalMediaFormatter(
                configs => { 
                    configs.AddConfiguration(typeof(Author), //Example adding a type config
                        new AuthorHalConfiguration());
                        });
        }
    }
```

3. In the HalConfig add HalTypeConfigurations by first specifying the type that will use it and then the associated configuration. 

## Registering the Hal Media Type Formatter

1. Open your Global.asax.cs file
2. Add the following line
 ```
  GlobalConfiguration.Configure(HalConfig.Register);
 ```
 
## Sample Request Response

### Request
```
GET api/author/2

Accept: application/hal+json
```
### Response
```
{
  "_links": {
    "self": {
      "href": "/api/author/2"
    },
    "find": {
      "href": "/api/author{?id}",
      "templated": true
    }
  },
  "Id": 2,
  "FirstName": "Charles",
  "LastName": "Dickens"
}
```
