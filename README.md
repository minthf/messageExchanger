# Message Exchanger
This project is designed for exchanging messages

## Architecture of web application
The project consists of 3 parts:
 - MessageServer
 - MessageClient
 - Database

### MessageServer
MessageServer is WebAPI ASP.NET application.
Architecture of project:
- Domain: contains of entities and interfaces of repositories.
- Infrastructure: contains of implementation of repositories.
- Application: contains of services.
- MessageServer: contains of controllers and configuration of application.

### MessageClient
MessageClient is ASP.NET MVC project. It represents a client for working with the server,
There is 3 controllers which represets every client.
- Sender: sends messages to the server.
- Reader: reads messages in real time using SignalR.
- ReaderByDate: reads messages from the server over a certain period of time.

### Database
The database chosen was PostgreSQL.
If you want to change database, you need to rewrite queries in MessageServer repository and change docker-compose file.

## Running application
There is docker-compose file, which uses images from docker hub, so you can get only **docker-compose.yaml** file and folder **initdb** with inital script for database, to run this application.
Make sure that ports which are in docker-compose.yaml are available.
If you will run this application locally you don`t need to change SignalRUrl in MessageClient appsettigns.json file. But if you want to deploy it on remote machine, you need to change it to your host url.