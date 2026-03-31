using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Cosmos;

namespace Costumes.API
{
    public static class UpdateCostume
    {
        [FunctionName("UpdateCostume")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "costumes/{id}")] HttpRequest req,
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
            log.LogInformation("UpdateCostume function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            string title = data?.title;
            string description = data?.description;
            int? spookyness = data?.spookyness;

            if (string.IsNullOrEmpty(title))
            {
                return new BadRequestObjectResult("Please pass a title in the request body");
            }

            if (costume == null)
            {
                return new NotFoundObjectResult("Boo! Costume not found.");
            }
            else
            {
                costume.title = title;
                costume.description = description;
                costume.spookyness = spookyness;

                var container = client.GetContainer("CostumesDB", "Costumes");
                string costumeId = costume.id.ToString();
                await container.ReplaceItemAsync<dynamic>(costume, costumeId, new PartitionKey(costumeId));

                return new OkObjectResult(costume);
            }
        }
    }
}
