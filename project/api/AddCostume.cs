using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Costumes.API;

public class AddCostumeOutput
{
    [CosmosDBOutput("CostumesDB", "Costumes", Connection = "CosmosDbConnectionString")]
    public object? Document { get; set; }

    public IActionResult HttpResponse { get; set; } = null!;
}

public class AddCostume(ILogger<AddCostume> logger)
{
    [Function("AddCostume")]
    public async Task<AddCostumeOutput> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "costumes")] HttpRequest req)
    {
        logger.LogInformation("Add Costume function processed a request.");

        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        dynamic? data = JsonConvert.DeserializeObject(requestBody);
        string? title = data?.title;
        string? description = data?.description;
        int? spookyness = data?.spookyness;

        var output = new AddCostumeOutput();

        if (!string.IsNullOrEmpty(title))
        {
            output.Document = new
            {
                id = Guid.NewGuid().ToString(),
                title,
                description,
                spookyness
            };
            output.HttpResponse = new OkObjectResult($"New costume: {title}, has been added to the database.");
        }
        else
        {
            output.HttpResponse = new OkObjectResult("Got your request but it didn't contain a title.");
        }

        return output;
    }
}
