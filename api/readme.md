# Toy Supplier Api

This is the api for the Toy Supplier sample project.

## Prerequisites

To run this app locally you'll need to install the [CosmosDB emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?tabs=docker-linux%2Ccsharp&pivots=api-nosql).

## Running locally

### Running the CosmosDB emulator

This contains instructions for running the CosmosDB emulator in a docker container. Running the emulator in docker can be a bit fiddly because you need to reinstall its SSL certificate every time the container is stopped and started (unless you use a persistent volume). A bash helper script `start_cosmos_emulator.sh` has been included which simplifies this process and allows running the emulator with a single command.

```sh
sh start_cosmos_emulator.sh
```

You can access the emulator portal at https://localhost:8081/_explorer/index.html


### Running the api

```sh
dotnet run
```
