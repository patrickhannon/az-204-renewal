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
command.SetHandler(
    handle: CosmosHandler.PartitionKeyBuilder, 
    nameOption, 
    emailOption,
    stateOption,
    countryOption
);

await command.InvokeAsync(args);