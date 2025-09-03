var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sqlServer.AddDatabase("VibeCrudDb");

builder.AddProject<Projects.VibeCrud_Web>("vibecrud-web")
    .WithReference(database, connectionName: "DefaultConnection")
    .WaitFor(database);

await builder.Build().RunAsync();
