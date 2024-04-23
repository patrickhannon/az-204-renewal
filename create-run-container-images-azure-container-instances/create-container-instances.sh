#!/bin/bash 
#echo ""

RED='\033[0;31m'
NC='\033[0m' # No Color

echo -e "${RED}Creating rg"
az group create --name az204-aci-rg --location centralus 

echo -e "${RED}Create a container"
DNS_NAME_LABEL=aci-example-$RANDOM

#echo -e "I ${RED}love${NC} Stack Overflow"
echo -e "${RED} Run the following az container create command to start a container instance with restart ${NC} InFailure"
echo "Add environmental variables"

az container create --resource-group az204-aci-rg \
    --file secure-env.yaml \
    --name mycontainer \
    --image mcr.microsoft.com/azuredocs/aci-helloworld \
    #--restart-policy OnFailure \
    #--environment-variables 'NumWords'='5' 'MinLength'='8'\
    --ports 80 \
    --dns-name-label $DNS_NAME_LABEL --location centralus

#<<'END_COMMENT'
echo -e "${RED}Verify the container is running"
az container show --resource-group az204-aci-rg \
    --name mycontainer \
    --query "{FQDN:ipAddress.fqdn,ProvisioningState:provisioningState}" \
    --out table

echo -e "${RED}Clean up resources"
#az group delete --name az204-aci-rg --no-wait
#END_COMMENT

#chmod +x create-container-instances.sh
#run ./create-container-instances.sh