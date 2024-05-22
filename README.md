# Message Exchanger
This project is designed for exchanging messages.

## Architecture of Web Application
The project consists of three parts:
- MessageServer
- MessageClient
- Database

### MessageServer
MessageServer is a WebAPI ASP.NET application.
The architecture of the project is as follows:
- Domain: Contains entities and repository interfaces.
- Infrastructure: Contains implementations of repositories.
- Application: Contains services.
- MessageServer: Contains controllers and application configuration.

### MessageClient
MessageClient is an ASP.NET MVC project. It represents a client for working with the server.
There are three controllers, each representing a different client function:
- Sender: Sends messages to the server.
- Reader: Reads messages in real time using SignalR.
- ReaderByDate: Reads messages from the server over a specified period of time.

### Database
The chosen database is PostgreSQL.
If you want to change the database, you need to rewrite the queries in the MessageServer repository and modify the docker-compose file accordingly.

## Running the Application
There is a docker-compose file that uses images from Docker Hub. You only need the **docker-compose.yaml** file and the **initdb** folder with the initial script for the database to run this application.
Make sure the ports specified in the docker-compose.yaml file are available.
If you run this application locally, you don't need to change the SignalRUrl in the MessageClient's appsettings.json file. However, if you want to deploy it on a remote machine, you need to update it with your host URL.
