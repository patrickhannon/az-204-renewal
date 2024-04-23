using Humanizer;
using Microsoft.Azure.Cosmos;

public static class CosmosHandler
{

    private static readonly CosmosClient _client;
    //"primaryMasterKey": "RAvOZEKcphHHVW3DJsCb2u7b8yzJn2abP6DsVjZYArVfUFBfZ4kXz5AWtGn1tc3JOVDzUGIpnvTfACDbOxEVMw==",
    //"documentEndpoint": "https://pjh-cosmos-db-yeah-centralus.documents.azure.com:443/",
    //https://learn.microsoft.com/en-us/azure/cosmos-db/nosql/tutorial-dotnet-console-app
    static CosmosHandler()
    {
        _client = new CosmosClient(
            accountEndpoint: "https://pjh-cosmos-db-yeah-centralus.documents.azure.com:443/",
            authKeyOrResourceToken: "YM6y9fj0seHHBxKdwQnKE6DyFVN5P7NlgGJwSVmaTcdNSpi894Dw7dFkVdtCVyL5Izp8gVfK5Q7AACDbhDXHog=="
        );
    }
    public static async Task CreateCustomerAsync(string name, string email, string state, string country)
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

        var response = await feed.ReadNextAsync();
        Console.WriteLine($"[{response.StatusCode}]\t{id}\t{response.RequestCharge} RUs");
    }

    public static async Task GetCustomerAsync(string name, string email, string state, string country)
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



        Console.WriteLine($"[{response.StatusCode}]\t{id}\t{response.RequestCharge} RU");
);
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