## Docker compose commands

We can combine two docker-compose files into one.
When you run docker compose up it reads the overrides automatically.

```
docker compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

```
docker compose -f docker-compose.yml -f docker-compose.override.yml down
```

We can also make "docker-compose.prod.yml" or "docker-compose.admin.yml"


## Important!

Since the app is in the container, I may not have access to some appsettings.

Therefore, it is crucial to have 
1. "ConnectionStrings": "mongodb://localhost:27017" in appsettings
2. - "DatabaseSettings__ConnectionString=mongodb://catalogdb:27017" in docker compose file
