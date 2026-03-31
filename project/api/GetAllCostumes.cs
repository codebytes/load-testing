using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Costumes.API;

public class GetAllCostumes(ILogger<GetAllCostumes> logger)
{
    [Function("GetAllCostumes")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "costumes")] HttpRequest req,
        [CosmosDBInput("CostumesDB", "Costumes",
            Connection = "CosmosDbConnectionString",
            SqlQuery = "SELECT * FROM c")] IEnumerable<dynamic> costumes)
    {
        logger.LogInformation("GetAllCostumes function processed a request.");
        return new OkObjectResult(costumes);
    }
}
