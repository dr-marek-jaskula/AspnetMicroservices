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