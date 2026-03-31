using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Costumes.API;

public class DeleteCostume(ILogger<DeleteCostume> logger, CosmosClient cosmosClient)
{
    [Function("DeleteCostume")]
    public async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "costumes/{id:guid}")] HttpRequest req,
        [CosmosDBInput("CostumesDB", "Costumes",
            Connection = "CosmosDbConnectionString",
            Id = "{id}",
            PartitionKey = "{id}")] dynamic? costume)
    {
        logger.LogInformation("DeleteCostume function processed a request.");

        if (costume == null)
        {
            return new NotFoundObjectResult("Boo! Costume not found.");
        }

        var container = cosmosClient.GetContainer("CostumesDB", "Costumes");
        string costumeId = costume.id.ToString();
        await container.DeleteItemAsync<dynamic>(costumeId, new PartitionKey(costumeId));

        return new OkObjectResult(costume);
    }
}
