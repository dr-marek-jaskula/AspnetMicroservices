# Redis

## Containerize Redis database

1. Pull redis image from DockerHub:

```bash
docker pull redis
```

2. Run redis image on port 6379 that is native for redis

```bash
docker run -d -p 6379:6379 --name aspnetrun-redis redis 
```

3. Examine that redis is running by

> docker ps

4. Open container bash in the terminal

> docker exec -it aspnetrun-redis /bin/bash

5. Use redis cli. Run:

> redis-cli

6. Examine by writing "ping"

Reminders:

Run in detached mode
> -d 

Port 6379 (this on the left) in the port in out computer, and it will forward calls to 6379 port in the container (on the right hand side)

Name of the mode
> --name 

Name of the image
> redis

Run in interactive mode
> -it
