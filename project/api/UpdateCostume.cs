using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Costumes.API;

public class UpdateCostume(ILogger<UpdateCostume> logger, CosmosClient cosmosClient)
{
    [Function("UpdateCostume")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "costumes/{id}")] HttpRequest req,
        [CosmosDBInput("CostumesDB", "Costumes",
            Connection = "CosmosDbConnectionString",
            Id = "{id}",
            PartitionKey = "{id}")] dynamic? costume)
    {
        logger.LogInformation("UpdateCostume function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic? data = JsonConvert.DeserializeObject(requestBody);
        string? title = data?.title;
        string? description = data?.description;
        int? spookyness = data?.spookyness;

        if (string.IsNullOrEmpty(title))
        {
            return new BadRequestObjectResult("Please pass a title in the request body");
        }

        if (costume == null)
        {
            return new NotFoundObjectResult("Boo! Costume not found.");
        }

        costume.title = title;
        costume.description = description;
        costume.spookyness = spookyness;

        var container = cosmosClient.GetContainer("CostumesDB", "Costumes");
        string costumeId = costume.id.ToString();
        await container.ReplaceItemAsync<dynamic>(costume, costumeId, new PartitionKey(costumeId));

        return new OkObjectResult(costume);
    }
}
