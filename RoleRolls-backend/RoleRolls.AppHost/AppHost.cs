var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.RoleRollsPocketEdition>("apiservice");

builder.AddExecutable("webfrontend", "npm", "..\\..\\frontend\\role-rolls", "start")
    .WithHttpEndpoint(port: 4300)
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
