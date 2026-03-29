var builder = DistributedApplication.CreateBuilder(args);

var postgresUser = builder.AddParameter("postgres-user", secret: true);
var postgresPassword = builder.AddParameter("postgres-password", secret: true);
var postgres = builder
    .AddPostgres("postgres", postgresUser, postgresPassword, 5432)
    .WithImage("postgres:17")
    .WithContainerName("doyep-analyzer-postgres")
    .WithVolume("doyep-analyzer-postgres-data", "/var/lib/postgresql/data")
    .AddDatabase("DoyepAnalyzerDb", "doyep-analyzer-db");

var api = builder
    .AddProject("api", "../../apps/api/Doyep.Analyzer.Api.csproj")
    .WithReference(postgres)
    .WaitFor(postgres)
    .WithExternalHttpEndpoints();

var web = builder
    .AddPnpmApp("web", "../../apps/web")
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
