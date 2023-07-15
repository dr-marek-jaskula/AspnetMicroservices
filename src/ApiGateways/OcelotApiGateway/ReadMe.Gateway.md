## Ocelot

1. Add logging to the application
2. Install Ocelot Nuget Package and then add ocelot to dependency container and add ocelot middleware
3. Add Three JSON files: ocelot.json, ocelot.Development.json, ocelot.Local.json
4. Change ASPNETCORE_ENVIRONMENT in launchSettings.json to Local
5. Add to program.cs "builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", true, true);"

## Configurations

```json
{
    "Routes": [
        {
        "DownstreamPathTemplate": "/todos/{id}",
        "DownstreamScheme": "https",
        "DownstreamHostAndPorts": [
            {
                "Host": "jsonplaceholder.typicode.com",
                "Port": 443
            }
        ],
        "UpstreamPathTemplate": "/todos/{id}",
        "UpstreamHttpMethod": [ "Get" ]
        }
    ],
    "GlobalConfiguration": {
        "BaseUrl": "https://localhost:5000"
    }
}
```

Upstreams are exposed to the external clients.

Downstreams are exposed to internal microservice architecture.

Each route must have upstream and downstream. BaseUrl is used for all streams.

## Rate Limiting

Late limiting options:
```json
"RateLimitOptions": {
    "ClientWhitelist": [], //clients in this list will not be affected by rate limiting
    "EnableRateLimiting": true,
    "Period": "3s", //period that the limits applies to
    "PeriodTimespan": 1, //value specifies that we can retry after a certain number of seconds
    "Limit": 1 //maximum number of requests in a period
```

We can put these configuration to global configurations:

```json
"RateLimitOptions": {
  "DisableRateLimitHeaders": false,
  "QuotaExceededMessage": "Customize Tips!",
  "HttpStatusCode": 999,
  "ClientIdHeader" : "Test"
}
```

DisableRateLimitHeaders - This value specifies whether X-Rate-Limit and Retry-After headers are disabled.

QuotaExceededMessage - This value specifies the exceeded message.

HttpStatusCode - This value specifies the returned HTTP Status code when rate limiting occurs.

ClientIdHeader - Allows you to specify the header that should be used to identify clients. By default it is “ClientId”

## Response caching

1. install NuGet Package: Ocelot.Cache.CacheManager

2. Add config

```csharp
s.AddOcelot()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    })
```

3. Adjust json file

```json
"FileCacheOptions": { "TtlSeconds": 15, "Region": "somename" }
```

In this example ttl seconds is set to 15 which means the cache will expire after 15 seconds.

## Docker environment 

We should name host names to container names and ports to default ones.

```json
//Catalog API
{
    "DownstreamPathTemplate": "/api/v1/Catalog",
    "DownstreamScheme": "http",
    "DownstreamHostAndPorts": [
    {
        "Host": "catalog.api", //we use container name
        "Port": "80" //we must change this do default one
    }
    ],
    "UpstreamPathTemplate": "/Catalog",
    "UpstreamHttpMethod": [ "GET", "POST", "PUT" ],
    "FileCacheOptions": { "TtlSeconds": 30 }
},
```
