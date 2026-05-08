using Microsoft.Extensions.Hosting;

var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder
    .AddPostgres("postgres",
        userName: builder.AddParameter("postgres-user", secret: true),
        password: builder.AddParameter("postgres-password", secret: true),
        port: 5432)
    .WithImage("postgres:17")
    .WithContainerName("doyep-analyzer-postgres")
    .WithVolume("doyep-analyzer-postgres-data", "/var/lib/postgresql/data")
    .AddDatabase("DoyepAnalyzerDb", "doyep-analyzer-db");

var redis = builder
    .AddRedis("redis");

var api = builder
    .AddProject("api", "../../apps/api/Doyep.Analyzer.Api.csproj")
    .WithReference(postgres)
    .WithReference(redis)
    .WaitFor(postgres)
    .WithHttpEndpoint()
    .WithExternalHttpEndpoints(); // TODO : verify if needed when docker compose is used

if (builder.Environment.IsDevelopment())
{
    api.WithUrlForEndpoint("http", ep => new()
    {
        Url = "/scalar",
        DisplayText = "Scalar API"
    });
}

var web = builder
    .AddPnpmApp("web", "../../apps/web")
    .WithPnpmPackageInstallation()
    .WithReference(api)
    .WithHttpEndpoint(port: 4200) // WithHttpEndpoint(env: "PORT") for dynamic port assignment 
    .WithMappedEndpointPort()
    .WithExternalHttpEndpoints(); // TODO : verify if needed when docker compose is used

api.WithEnvironment("Web__BaseUrl", web.GetEndpoint("http"));

builder.Build().Run();
