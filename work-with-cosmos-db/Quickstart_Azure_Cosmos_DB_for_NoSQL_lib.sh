#!/bin/bash 

#chmod +x Quickstart_Azure_Cosmos_DB_for_NoSQL_library_for_.NET.sh
#run ./Quickstart_Azure_Cosmos_DB_for_NoSQL_library_for_.NET.sh

#https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/quickstart-dotnet?pivots=devcontainer-vscode

# (✓) Done: Resource group: rg-az204-cosmos-pjh

#You can view detailed progress in the Azure Portal:
#https://portal.azure.com/#view/HubsExtension/DeploymentDetailsBlade/~/overview/id/%2Fsubscriptions%2Fa07e1329-e13a-4c90-babc-03a38e8d3000%2Fproviders%2FMicrosoft.Resources%2Fdeployments%2Faz204-cosmos-pjh-1713626481

RED='\033[0;31m'
NC='\033[0m' # No Color


azd auth login --use-device-code
#AEALURK7V

azd init

azd up

#chmod +x Quickstart_Azure_Cosmos_DB_for_NoSQL_library_for_.NET.sh
#run ./Quickstart_Azure_Cosmos_DB_for_NoSQL_library_for_.NET.sh

#https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd?tabs=winget-windows%2Cbrew-mac%2Cscript-linux&pivots=os-linux
#https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/tutorial-dotnet-console-app

curl -fsSL https://aka.ms/install-azd.sh | bash

curl -fsSL https://aka.ms/install-azd.sh | bash


dotnet new console --langVersion preview

dotnet add package Microsoft.Azure.Cosmos --version 3.31.1-preview

dotnet add package System.CommandLine --prerelease

dotnet add package Humanizer

dotnet run -- --name 'Patrick Hannon --state 'Texas' --country 'United States'

dotnet run -- --name 'Patrick Hannon' --state 'Washington' --country 'United States'
#[OK]    patrick-hannon  2.82 RUs