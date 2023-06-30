## Portainer

Portiner Community Edition (ce) is free

Add to docker-compose:

```yml
portainer:
	image: portainer/portainer-ce
```

```yml
  portiner:
    container_name: portainer
    restart: always
    ports:
      - "8080:8000"
      - "9000:9000"
    volumes:
      - /var/run/docker.sock:/var/run/docker.sock
      - portainer_data:/data
```

Remember user credntials, there is no default user:

login: admin
password: admin1234567
