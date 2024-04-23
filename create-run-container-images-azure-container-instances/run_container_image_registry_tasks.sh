#!/bin/bash 

RED='\033[0;31m'
NC='\033[0m' # No Color

echo -e "${RED}Create a resource group for the registry ${NC} "
az group create --name az204-acr-rg --location centralus

echo -e "${RED}Create a basic container registry ${NC} "
az acr create --resource-group az204-acr-rg \
    --name pjh1234container --sku Basic

#chmod +x run_container_image_registry_tasks.sh
#run ./run_container_image_registry_tasks.sh

echo -e "${RED}Build and push image from a Dockerfile ${NC} "
echo FROM mcr.microsoft.com/hello-world > Dockerfile

az acr repository list --name pjh1234container --output table

echo -e "${RED}az acr repository show-tags command to list the tags on the sample/hello-world repository. ${NC} "
az acr repository show-tags --name pjh1234container \
                            --repository sample/hello-world \
                            --output table

echo -e "${RED}run the az acr build command, which builds the image and, after the image is successfully built, pushes it to your registry ${NC} "
az acr build --image sample/hello-world:v1 \
             --registry pjh1234container \
             --file Dockerfile .

echo -e "${RED}Run the image in the ACR Todo ${NC} "
az acr run --registry pjh1234container \
    --cmd '$Registry/sample/hello-world:v1' /dev/null

#az group delete --name az204-acr-rg --no-wait 
