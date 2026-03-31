using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Costumes.API;

public class GetCostumeById(ILogger<GetCostumeById> logger)
{
    [Function("GetCostumeById")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "costumes/{id:guid}")] HttpRequest req,
        [CosmosDBInput("CostumesDB", "Costumes",
            Connection = "CosmosDbConnectionString",
            Id = "{id}",
            PartitionKey = "{id}")] dynamic? costume)
    {
        logger.LogInformation("GetCostumeById function processed a request.");

        if (costume == null)
        {
            return new NotFoundObjectResult("Boo! Costume not found.");
        }

        return new OkObjectResult(costume);
    }
}
