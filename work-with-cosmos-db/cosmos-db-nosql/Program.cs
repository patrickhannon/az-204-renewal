// See https://aka.ms/new-console-template for more information
using System.CommandLine;

var command = new RootCommand();

var nameOption = new Option<string>("--name") { IsRequired = true };
var emailOption = new Option<string>("--email");
var stateOption = new Option<string>("--state") { IsRequired = true };
var countryOption = new Option<string>("--country") { IsRequired = true };

command.AddOption(nameOption);
command.AddOption(emailOption);
command.AddOption(stateOption);
command.AddOption(countryOption);


//Create
/*
command.SetHandler(
    handle: CosmosHandler.CreateCustomerAsync, 
    nameOption, 
    emailOption,
    stateOption,
    countryOption
);
*/
//Get
/*
command.SetHandler(
    handle: CosmosHandler.GetCustomerAsync, 
    nameOption, 
    emailOption,
    stateOption,
    countryOption
);
*/
//Partition key PartitionKeyBuilder
//Create_a_transaction_using_the_SDK

//dotnet run -- --name 'Patrick Hannon' --state 'Washington' --country 'United States'
command.SetHandler(
    //handle: CosmosHandler.Add_items_to_a_container_using_the_SDK, 
    //dotnet run -- --name 'Patrick Hannon' --state 'Texas' --country 'United States'
    handle: CosmosHandler.Retrieve_an_item_using_the_SDK, 
    //dotnet run -- --name 'Shannie Hannon' --state 'Texas' --country 'United States'
    //handle: CosmosHandler.PartitionKeyBuilder, 
    //dotnet run -- --name 'Patrick Hannon' --state 'Washington' --country 'United States'
    //handle: CosmosHandler.Create_a_transaction_using_the_SDK, 
    //dotnet run -- --name 'Patrick Hannon' --state 'Washington' --country 'United States'
    nameOption, 
    emailOption,
    stateOption,
    countryOption
);

await command.InvokeAsync(args);