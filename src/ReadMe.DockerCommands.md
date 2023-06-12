## Interesting docker commands

Stop all
> docker stop $(docker ps -aq)

Remove all
> docker rm $(docker ps -aq)

Remove unnamed docker images
> docker system prune

Run with override
> docker compose -f .\docker-compose.yml -f .\docker-compose.override.yml up

And stop with override
> docker compose -f .\docker-compose.yml -f .\docker-compose.override.yml down
