#!/bin/bash 

#chmod +x 3-exercise-work-cosmos-db-dotnet.sh
#run ./3-exercise-work-cosmos-db-dotnet.sh

#https://learn.microsoft.com/en-us/training/modules/work-with-cosmos-db/3-exercise-work-cosmos-db-dotnet

az login

RED='\033[0;31m'
NC='\033[0m' # No Color

#PoC subscription

echo -e "${RED}Set Subscription to PoC command: az account set --subscription "a07e1329-e13a-4c90-babc-03a38e8d3000" {NC} "
az account set --subscription "a07e1329-e13a-4c90-babc-03a38e8d3000"


echo -e "${RED}Create a resource group for the cosmos command az group create --location centralus --name c ${NC} "
az group create --location centralus --name rg-az204-cosmos-pjh

read -p "Press enter to continue"

echo -e "${RED}Creating the Azure Cosmos DB account. command: az cosmosdb create --name pjh-cosmos-db-yeah --resource-group rg-az204-cosmos-pjh$ {NC} "
#az cosmosdb create --name pjh-cosmos-db-yeah --resource-group rg-az204-cosmos-pjh

# Create a free tier account for API for NoSQL
az cosmosdb create \
    -n pjh-cosmos-db-yeah \
    -g rg-az204-cosmos-pjh \
    --enable-free-tier true \
    --default-consistency-level "Session"

read -p "Press enter to continue"

echo -e "${RED}# Retrieve the primary key ${NC} "
az cosmosdb keys list --name pjh-cosmos-db-yeah --resource-group rg-az204-cosmos-pjh

read -p "Press enter to continue"

echo -e "${RED}Created the Azure Cosmos DB account and retrieved primary key ${NC} "

<<'END_COMMENT'

echo -e "${RED}# Set up the console application ${NC} "
md az204-cosmos
cd az204-cosmos


echo -e "${RED}Create the .NET console app. ${NC} "
az cosmosdb keys list --name pjh-cosmos-db-yeah --resource-group rg-az204-cosmos-pjh

echo -e "${RED}Open the current folder in Visual Studio Code. Requires input ${NC} "
#code . -r

echo -e "${RED}Build the console app ${NC} "
echo -e "${RED}Add packages and using statements${NC} "

#dotnet add package Microsoft.Azure.Cosmos

END_COMMENT

#az group delete --name rg-az204-cosmos-pjh --no-wait 