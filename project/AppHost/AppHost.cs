var builder = DistributedApplication.CreateBuilder(args);

// CosmosDB - uses emulator locally, Azure CosmosDB in production
var cosmos = builder.AddAzureCosmosDB("cosmos")
    .RunAsEmulator()
    .AddCosmosDatabase("CostumesDB");

// Azure Functions API - .NET 10 isolated worker
var api = builder.AddAzureFunctionsProject<Projects.azltops_func>("costumes-api")
    .WithReference(cosmos)
    .WaitFor(cosmos);

// Vue.js frontend webapp
builder.AddNpmApp("costumes-webapp", "../webapp")
    .WithReference(api)
    .WithHttpEndpoint(env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
