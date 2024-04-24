using Humanizer;
using Microsoft.Azure.Cosmos;

public static class CosmosHandler
{

    private static readonly CosmosClient _client;
    //https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/tutorial-dotnet-console-app
    static CosmosHandler()
    {
        _client = new CosmosClient(
            accountEndpoint: "https://pjh-cosmos-db-yeah-centralus.documents.azure.com:443/",
            authKeyOrResourceToken: "YM6y9fj0seHHBxKdwQnKE6DyFVN5P7NlgGJwSVmaTcdNSpi894Dw7dFkVdtCVyL5Izp8gVfK5Q7AACDbhDXHog=="
        );
    }
    public static async Task Add_items_to_a_container_using_the_SDK(string name, string email, string state, string country)
    {
        //await Console.Out.WriteLineAsync($"Hello {name} of {state}, {country}!");
        Container container = await GetContainerAsync();
        string id = name.Kebaberize();
        var customer = new
        {
            id = id,
            name = name,
            address = new
            {
                state = state,
                country = country
            }
        };

        var response = await container.CreateItemAsync(customer);

        Console.WriteLine($"[{response.StatusCode}]\t{id}\t{response.RequestCharge} RUs");
    }
    //fatal: Unable to create '/home/phannon/cloud/azure/az-204-renewal/.git/index.lock': File exists.
    public static async Task Retrieve_an_item_using_the_SDK(string name, string email, string state, string country)
    {
        //await Console.Out.WriteLineAsync($"Hello {name} of {state}, {country}!");
        Container container = await GetContainerAsync();
        string id = name.Kebaberize();

        string sql = """
            SELECT
                *
            FROM customers c
            WHERE c.id = @id
            """;

        var query = new QueryDefinition(
        query: sql
        )
    .WithParameter("@id", id);

        using var feed = container.GetItemQueryIterator<dynamic>(
        queryDefinition: query
    );
        var response = await feed.ReadNextAsync();
        Console.WriteLine($"[{response.StatusCode}]\t{id}\t{response.RequestCharge} RUs");
    }

    public static async Task PartitionKeyBuilder(string name, string email, string state, string country)
    {
        //await Console.Out.WriteLineAsync($"Hello {name} of {state}, {country}!");
        Container container = await GetContainerAsync();
        string id = name.Kebaberize();

        var partitionKey = new PartitionKeyBuilder()
        .Add(country)
        .Add(state)
        .Build();

        var response = await container.ReadItemAsync<dynamic>(
        id: id,
        partitionKey: partitionKey
        );


        Console.WriteLine($"[{response.StatusCode}]\t{id}\t{response.RequestCharge} RU");

    }

    public static async Task Create_a_transaction_using_the_SDK(string name, string email, string state, string country)
    {
        Container container = await GetContainerAsync();
        string id = name.Kebaberize();


        var customerCart = new
        {
            id = $"{Guid.NewGuid()}",
            customerId = id,
            items = new string[] { },
            address = new
            {
                state = state,
                country = country
            }
        };

        var customerContactInfo = new
        {
            id = $"{id}-contact",
            customerId = id,
            email = email,
            location = $"{state}, {country}",
            address = new
            {
                state = state,
                country = country
            }
        };

        var partitionKey = new PartitionKeyBuilder()
                .Add(country)
                .Add(state)
                .Build();

        var batch = container.CreateTransactionalBatch(partitionKey)
            .ReadItem(id)
            .CreateItem(customerCart)
            .CreateItem(customerContactInfo);

        using var response = await batch.ExecuteAsync();

        Console.WriteLine($"[{response.StatusCode}]\t{response.RequestCharge} RUs");

    }

    private static async Task<Container> GetContainerAsync()
    {
        Database database = _client.GetDatabase("cosmicworks");
        List<string> keyPaths = new()
        {
            "/address/country",
            "/address/state"
        };
        ContainerProperties properties = new(
            id: "customers",
            partitionKeyPaths: keyPaths
        );

        return await database.CreateContainerIfNotExistsAsync(
            containerProperties: properties
        );
    }

}