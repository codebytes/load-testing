using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Costumes.API;

public class GetCostumeByTitle(ILogger<GetCostumeByTitle> logger)
{
    [Function("GetCostumeByTitle")]
    public IActionResult Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "costumes/{title:alpha}")] HttpRequest req,
        [CosmosDBInput("CostumesDB", "Costumes",
            Connection = "CosmosDbConnectionString",
            SqlQuery = "SELECT * FROM c WHERE RegexMatch(c.title, {title}, 'i')")] IEnumerable<dynamic>? costumes)
    {
        logger.LogInformation("GetCostumeByTitle function processed a request.");

        if (costumes == null) return new NotFoundResult();

        return new OkObjectResult(costumes);
    }
}
