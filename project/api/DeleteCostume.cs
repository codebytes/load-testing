using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Cosmos;

namespace Costumes.API
{
    public static class DeleteCostume
    {
        [FunctionName("DeleteCostume")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "costumes/{id:Guid}")] HttpRequest req,
            [CosmosDB(
                databaseName: "CostumesDB",
                containerName: "Costumes",
                Connection = "CosmosDbConnectionString",
                Id = "{id}",
                PartitionKey = "{id}")] dynamic costume,
            [CosmosDB(
                databaseName: "CostumesDB",
                containerName: "Costumes",
                Connection = "CosmosDbConnectionString")] CosmosClient client,
            ILogger log)
        {
            log.LogInformation("DeleteCostume function processed a request.");

            if (costume == null)
            {
                return new NotFoundObjectResult("Boo! Costume not found.");
            }
            else
            {
                var container = client.GetContainer("CostumesDB", "Costumes");
                string costumeId = costume.id.ToString();
                await container.DeleteItemAsync<dynamic>(costumeId, new PartitionKey(costumeId));

                return new OkObjectResult(costume);
            }
        }
    }
}
