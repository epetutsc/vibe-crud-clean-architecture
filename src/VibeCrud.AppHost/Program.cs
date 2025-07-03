var builder = DistributedApplication.CreateBuilder(args);

// Add SQL Server
var sqlServer = builder.AddSqlServer("sql")
    .WithDataVolume();

var database = sqlServer.AddDatabase("VibeCrudDb");

// Add the web application
var webApp = builder.AddProject("webfrontend", "../VibeCrud.Web/VibeCrud.Web.csproj")
    .WithReference(database);

builder.Build().Run();