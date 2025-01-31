#!/bin/bash

start_cosmos_emulator() {
    CONTAINER_ID=$(docker ps -aqf "name=cosmosdb-emulator")
    if [ -z "$CONTAINER_ID" ]; then
        echo "Starting a new CosmosDB emulator container..."
        docker run -d --name=cosmosdb-emulator \
            -p 8081:8081 -p 8900-8914:8900-8914 \
            -e AZURE_COSMOS_EMULATOR_PARTITION_COUNT=2 \
            -e AZURE_COSMOS_EMULATOR_ENABLE_DATA_PERSISTENCE=true \
            mcr.microsoft.com/cosmosdb/linux/azure-cosmos-emulator
    else
        echo "Starting existing CosmosDB emulator container..."
        docker start cosmosdb-emulator
    fi

    # Wait for the emulator to start
    sleep 5

    # Get certificate from container and install it
    curl -k https://localhost:8081/_explorer/emulator.pem --output cosmos-emulator-cert.pem
    sudo mv cosmos-emulator-cert.pem /usr/local/share/ca-certificates/cosmos-emulator.crt
    sudo update-ca-certificates

    echo "CosmosDB Emulator started and certificate installed."
    echo
    echo "You can view CosmosDB instance here: https://localhost:8081/_explorer/index.html"
}

start_cosmos_emulator