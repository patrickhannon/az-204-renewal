#https://learn.microsoft.com/en-us/training/modules/implement-azure-container-apps/3-exercise-deploy-app
#Exercise - Deploy a container app
#Install the Azure Container Apps extension for the CLI.
az extension add --name containerapp --upgrade
#Register the Microsoft.App namespace
az provider register --namespace Microsoft.App
#Register the Microsoft.OperationalInsights
az provider register --namespace Microsoft.OperationalInsights
#Set environment variables used later in this exercise. Replace <location> with a region near you.
myRG=az204-appcont-rg
myLocation=centralus 
myAppContEnv=az204-env-$RANDOM
#Create the resource group for your container app.
az group create --name $myRG --location $myLocation
#Create an environment
az containerapp env create --name $myAppContEnv --resource-group $myRG --location $myLocation
#Create a container app
az containerapp create --name my-container-app --resource-group $myRG --environment $myAppContEnv --target-port 80 --ingress 'external' --query properties.configuration.ingress.fqdn
#"https://my-container-app.purplepond-26fe2d6f.centralus.azurecontainerapps.io"
#Clean up resources
az group delete --name $myRG
