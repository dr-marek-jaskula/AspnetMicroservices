# MongoDb

## Containerize Mongo database

1. Pull mongo image from DockerHub:

```bash
docker pull mongo
```

2. Run mongo image on port 27017 that is native for mongo

```bash
docker run -d -p 27017:27017 --name shopping-mongo mongo
```

3. Examine that mongo is running by

> docker ps

4. Open container bash in the terminal

> docker exec -it shopping-mongo /bin/bash

5. Use mongo shell (mongosh)

Reminders:

Run in detached mode
> -d 

Port 27017 (this on the left) in the port in out computer, and it will forward calls to 27017 port in the container (on the right hand side)

Name of the mode
> --name 

Name of the image
> mongo

Run in interactive mode
> -it
